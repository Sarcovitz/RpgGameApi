using Microsoft.Extensions.Options;
using RpgGame.Configuration;
using RpgGame.Providers.Interfaces;
using System.Net;
using System.Net.Mail;

namespace RpgGame.Providers;

public class SmtpClientProvider : ISmtpClientProvider
{
    private readonly EmailConfig _emailConfig;
    private readonly SmtpClient _smtpClient;

    public SmtpClientProvider(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig.Value;
        _smtpClient = new SmtpClient(_emailConfig.SmtpHost)
        {
            Port = _emailConfig.Port,
            UseDefaultCredentials = _emailConfig.UseDefaultCredentials,
            Credentials = new NetworkCredential()
            {
                UserName = _emailConfig.EmailAddress,
                Password = _emailConfig.EmailPassword
            }
        };
    }

    public void Send(MailMessage msg)
    {
        _smtpClient.SendAsync(msg, null);
    }
}
