﻿using RpgGame.Models.Entity;
using System.Net.Mail;
using System.Net;
using System.Text;
using RpgGame.Services.Interfaces;
using Microsoft.Extensions.Options;
using RpgGame.Configuration;

namespace RpgGame.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfig _emailConfig;
    private readonly AppConfig _appConfig;
    private readonly SmtpClient _smtpClient;

    public EmailService(IOptions<AppConfig> appConfig, IOptions<EmailConfig> emailConfig)
    {
        _appConfig = appConfig.Value;
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

    public void SendAccountConfirmationEmail(User user)
    {
        try
        {
            string userDataRaw = $"{user.Username}:{user.Id}:{user.Email}";
            string userData = Convert.ToBase64String(Encoding.UTF8.GetBytes(userDataRaw));
            string userDataUrl = WebUtility.UrlEncode(userData);

            MailAddress mailFrom = new(_emailConfig.EmailAddress, "RPG GAME");
            MailAddress mailTo = new(user.Email, user.Username.ToUpper());

            MailMessage msg = new(mailFrom, mailTo)
            {
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
                Subject = "Confirmation Mail",
                Body = $"<h1><a href=\"{_appConfig.Url}/auth/confirm-account/{userDataUrl}\"> Confirm Account </a></h1>",
                IsBodyHtml = true
            };

            _smtpClient.SendAsync(msg, null);
        }
        catch 
        {
            throw new Exception($"Error while sending confirmation e-mail. Address: {user.Email}");
        }
    }
}
