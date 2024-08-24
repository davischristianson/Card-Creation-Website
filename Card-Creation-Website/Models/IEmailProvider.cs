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

        public async Task SendEmailAsync(string email, string fromEmail, string subject, string content, string htmlContent)
        {
            // SendGrid Email Test
            var apiKey = _config.GetSection("SendGridKey").Value;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("kejegamer@gmail.com", "KG Team"),
                Subject = "Testing again, test test test",
                PlainTextContent = "This is a test email",
                HtmlContent = "<strong>Testing email test test</strong>"
            };
            msg.AddTo(new EmailAddress("kejegamer@gmail.com", "Keje"));
            await client.SendEmailAsync(msg);
            // var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
