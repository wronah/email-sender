using EmailSender.Api.DTOs.Mail;
using EmailSender.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmailSender.Api.Controllers;

[ApiController]
[Route("mail")]
public class MailController(IEmailService emailService) : ControllerBase
{
    [HttpPost("send")]
    [Authorize]
    public async Task<ActionResult<SendResponse>> SendEmail([FromBody] SendRequest request)
    {

        var emailSent = await emailService.SendAsync(request.To, request.Subject, request.Body);

        return Ok(new SendResponse
        {
            AppId = User.FindFirstValue("appId") ?? string.Empty,
            AppName = User.FindFirstValue("appName") ?? string.Empty,
            Status = emailSent ? "success" : "failed",
            Email = new Info
            {
                To = request.To,
                Subject = request.Subject,
                Body = request.Body,
            }
        });
    }
}
