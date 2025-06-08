namespace EmailSender.Api.DTOs.ClientApp;

public class RegisterRequest
{
    public string AppId { get; set; } = string.Empty;
    public string AppName { get; set; } = string.Empty;
    public string Pass { get; set; } = string.Empty;
}
