using Market.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Market.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SmtpSettings> smtpSettings, ILogger<EmailService> logger)
        {
            _smtpSettings = smtpSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                _logger.LogInformation($"Attempting to send email to {email}");

                var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    Credentials = new System.Net.NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage(_smtpSettings.From, email, subject, message)
                {
                    IsBodyHtml = true
                };

                await client.SendMailAsync(mailMessage);

                _logger.LogInformation($"Email sent successfully to {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {email}");
                throw; // Re-throw the exception to be handled by the caller
            }
        }
    }
}
