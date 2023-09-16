namespace RpgGame.Configuration;

public class EmailConfig
{
    public string SmtpHost { get; set; } = string.Empty;
    public int Port { get; set; } = 0;
    public bool UseDefaultCredentials { get; set; } = false;
    public string EmailAddress { get; set; } = string.Empty;
    public string EmailPassword { get; set; } = string.Empty;
}
