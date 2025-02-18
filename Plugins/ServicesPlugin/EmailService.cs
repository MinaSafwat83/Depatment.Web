﻿using DepartmentModule.UseCase.PluginInterface.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;


public class EmailSettings
{
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; }
    public string SmtpPass { get; set; }
    public string SenderEmail { get; set; }
    public string SenderName { get; set; }
}

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, false);
            await client.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}


