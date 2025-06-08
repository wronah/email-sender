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
            using var client = new SmtpClient(configuration["Smtp:Host"], Int32.Parse(configuration["Smtp:Port"]!))
            {
                Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]),
                EnableSsl = Convert.ToBoolean(configuration["Smtp:EnableSsl"])
            };

            var mailMessage = new MailMessage(
                new MailAddress(configuration["Smtp:FromEmail"]!,
                configuration["Smtp:FromName"]).ToString(),
                to,
                subject,
                body
            );

            await client.SendMailAsync(mailMessage);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
