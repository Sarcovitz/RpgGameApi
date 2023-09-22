﻿using Microsoft.Extensions.Options;
using RpgGame.Configuration;
using RpgGame.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGameApiTests.ServicesTests;

[TestFixture]
public class EmailServiceTests
{
    private IEmailService _emailService;

    private IOptions<AppConfig> _appOptions;
    private IOptions<EmailConfig> _emailOptions;
    private Mock<ISmtpClientProvider> _smtpClientMock;

    [SetUp]
    public void Setup()
    {
        _appOptions = Options.Create(new AppConfig()
        {
            Url = "http://address:4200"
        });

        _emailOptions = Options.Create(new EmailConfig()
        {
            EmailAddress = "test@test.com",
            EmailPassword = "P@$$word",
            Port = 123,
            SmtpHost = "localhost",
            UseDefaultCredentials = false
        });

        _smtpClientMock = new Mock<ISmtpClientProvider>();

        _emailService = new EmailService(_appOptions, _emailOptions, _smtpClientMock.Object);
    }

    public void SetupEmailService()
    {
        _emailService = new EmailService(_appOptions, _emailOptions, _smtpClientMock.Object);
    }

    [Test]
    public void SendAccountConfirmationEmail_OnSuccess()
    {
        User? user = new()
        {
            Username = "Username",
            Id = 123,
            Email = "test@test.pl",
        };

        bool result = _emailService.SendAccountConfirmationEmail(user);

        Assert.That(result, Is.True);
    }


}
