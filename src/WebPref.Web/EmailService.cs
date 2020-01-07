using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace WebPref.Web
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация сервера онлайн-преферанса", "webpref@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true; //TODO - разобраться с проверкой сертификатов, пока нам это не очень важно

                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync("webpref", "Pre3fe5rence");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
