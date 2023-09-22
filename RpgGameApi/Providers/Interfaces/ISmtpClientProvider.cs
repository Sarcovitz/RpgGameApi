using System.Net.Mail;

namespace RpgGame.Providers.Interfaces;

public interface ISmtpClientProvider
{
    void SendAsync(MailMessage msg);
}
