namespace EmailSender.Api.DTOs.Mail;

public class SendResponse
{
    public string AppId { get; set; } = string.Empty;
    public string AppName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public Info Email { get; set; } = new();
}
