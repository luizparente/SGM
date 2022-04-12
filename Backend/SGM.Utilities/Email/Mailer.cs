using Orion.Utilities.Configuration;
using Orion.Utilities.Email.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Orion.Utilities.Email {
    /// <summary>
    /// An email helper class that loads its basic configurations from orionsettings.json.
    /// </summary>
    public class Mailer : IMailer {
        private readonly string host;
        private readonly int port;
        private readonly string sender;

        public Mailer() {
            this.host = ConfigurationManager.Settings["SMTPHost"];
            this.port = int.Parse(ConfigurationManager.Settings["SMTPPort"]);
            this.sender = ConfigurationManager.Settings["SenderEmailAddress"];
        }

        /// <summary>
        /// Sends an email to the specified recipient.
        /// </summary>
        /// <param name="recipient">The email address of the recipient.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="htmlBody">The HTML body of the message.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SendEmailAsync(string recipient, string subject, string htmlBody) {
            var client = new SmtpClient(this.host, this.port);

            await client.SendMailAsync(new MailMessage(this.sender, recipient, subject, htmlBody) {
                IsBodyHtml = true
            });
        }

        /// <summary>
        /// Sends an email to the specified recipient list.
        /// </summary>
        /// <param name="recipients">The email addresses of the recipients.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="htmlBody">The HTML body of the message.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task SendEmailAsync(string[] recipients, string subject, string htmlBody) {
            var client = new SmtpClient(this.host, this.port);
            var message = new MailMessage();
            message.Sender = new MailAddress(this.sender);
            message.From = new MailAddress(this.sender);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = htmlBody;

            foreach (string recipient in recipients) {
                message.To.Add(recipient);
            }

            await client.SendMailAsync(message);
        }
    }
}