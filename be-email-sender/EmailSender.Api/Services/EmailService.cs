using EmailSender.Api.Interfaces;
using System.Net.Mail;
using System.Net;

namespace EmailSender.Api.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task<bool> SendAsync(string to, string subject, string body)
    {
        try
        {
            using var client = new SmtpClient(configuration["Smtp:Host"], Int32.Parse(configuration["Smtp:Port"]!));
            client.Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]);
            client.EnableSsl = Convert.ToBoolean(configuration["Smtp:EnableSsl"]);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration["Smtp:FromEmail"]!, configuration["Smtp:FromName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
