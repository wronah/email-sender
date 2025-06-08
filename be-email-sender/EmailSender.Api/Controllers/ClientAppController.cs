using EmailSender.Api.DTOs.ClientApp;
using EmailSender.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Api.Controllers;

[ApiController]
[Route("client-app")]
public class ClientAppController(IJwtService jwtService) : ControllerBase
{
    private const string validPassword = "q##waQBh";

    [HttpPost("register")]
    public ActionResult<RegisterResponse> Register([FromBody] RegisterRequest request)
    {
        if(request.Pass != validPassword)
        {
            return BadRequest("Password is invalid");
        }

        var token = jwtService.GenerateToken(request.AppId, request.AppName);

        return Ok(new RegisterResponse
        {
            AppId = request.AppId,
            AppName = request.AppName,
            Key = token
        });
    }
}
