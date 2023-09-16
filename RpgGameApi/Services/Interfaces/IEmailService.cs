using RpgGame.Models.Entity;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace RpgGame.Services.Interfaces;

public interface IEmailService
{
    Task SendAccountConfirmationEmailAsync(User user);
}
