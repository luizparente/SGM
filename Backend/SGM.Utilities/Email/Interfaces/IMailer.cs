using System.Threading.Tasks;

namespace Orion.Utilities.Email.Interfaces {
    public interface IMailer {
        /// <summary>
        /// Sends an email to the specified recipient.
        /// </summary>
        /// <param name="recipient">The email address of the recipient.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="htmlBody">The HTML body of the message.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public Task SendEmailAsync(string recipient, string subject, string htmlBody);

        /// <summary>
        /// Sends an email to the specified recipient list.
        /// </summary>
        /// <param name="recipient">The email addresses of the recipients.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="htmlBody">The HTML body of the message.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public Task SendEmailAsync(string[] recipients, string subject, string htmlBody);
    }
}
