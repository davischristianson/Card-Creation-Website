using Card_Creation_Website.Data;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Card_Creation_Website.Models
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(string email, string fromEmail, string subject, string content, string htmlContent);
    }

    public class EmailProviderSendGrid : IEmailProvider
    {
        private readonly IConfiguration _config;
        public EmailProviderSendGrid(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="fromEmail"></param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        /// <param name="htmlContent"></param>
        /// Might need to add a parameter that takes in first and 
        /// last name so the new email address can have the name on it
        /// <returns></returns>
        /// 
        /// public async Task SendEmailAsync(string email, string fromEmail, string subject, string content, string htmlContent, string FullName)
        public async Task SendEmailAsync(string email, string fromEmail, string subject, string content, string htmlContent)
        {
            // SendGrid Email Test
            var apiKey = _config.GetSection("SendGridKey").Value;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                // What a real email should look like
                // Should grab the domain email, can hard code this preferably since this won't change very much
                // From = new EmailAddress("domain@domain.com", "domain"),
                // Subject = subject,
                // PlainTextContent = content,
                // HtmlContent = htmlContent

                From = new EmailAddress("kejegamer@gmail.com", "KG Team"),
                Subject = subject,
                PlainTextContent = content,
                HtmlContent = htmlContent
            };
            // Correct destination to the recipient
            // msg.AddTo(new EmailAddress(email, FullName));
            msg.AddTo(new EmailAddress("kejegamer@gmail.com", "Keje"));
            await client.SendEmailAsync(msg);
            // var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
