using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.BLL.Settings;
using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly ILogger<EmailService> _logger;
        private const string _fromAdressTitle = "Pizza Portal";
        private const string _toAdressTitle = "Microsoft ASP.NET Core";

        public EmailService(IEmailConfiguration emailConfiguration, ILogger<EmailService> logger)
        {
            _emailConfiguration = emailConfiguration;
            this._logger = logger;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress(_fromAdressTitle, this._emailConfiguration.SmtpUsername));

                mimeMessage.To.Add(new MailboxAddress(_toAdressTitle, emailMessage.ToAddress));

                mimeMessage.Subject = emailMessage.Subject;

                mimeMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailMessage.Content
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(this._emailConfiguration.SmtpServer, this._emailConfiguration.SmtpPort);

                    client.Authenticate(this._emailConfiguration.SmtpUsername, this._emailConfiguration.SmtpPassword);

                    await client.SendAsync(mimeMessage);

                    this._logger.LogInformation($"The mail has been sent successfully. To: {emailMessage.ToAddress}");

                    await client.DisconnectAsync(true);
                }
        }
    }
}
