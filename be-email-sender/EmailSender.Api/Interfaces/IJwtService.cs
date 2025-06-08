namespace EmailSender.Api.Interfaces;

public interface IJwtService
{
    string GenerateToken(string appId, string appName);
}
