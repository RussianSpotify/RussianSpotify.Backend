#region

using Microsoft.Extensions.Configuration;
using MimeKit;
using RussianSpotify.API.Shared.Interfaces;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


#endregion

namespace RussianSpotify.API.Shared.Services;

/// <summary>
///     Отвечает за отправку писем на почту
/// </summary>
public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
        => _configuration = configuration;

    /// <inheritdoc cref="IEmailSender" />
    public async Task SendEmailAsync(string to, string message, CancellationToken cancellationToken)
    {
        var emailConfiguration = _configuration.GetSection("EmailSettings");

        using var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(emailConfiguration["FromName"],
            emailConfiguration["EmailAddress"]));

        emailMessage.To.Add(new MailboxAddress("", to));

        emailMessage.Subject = emailConfiguration["FromName"];

        var bodyBuilder = new BodyBuilder { HtmlBody = message };

        var body = bodyBuilder.ToMessageBody();

        emailMessage.Body = body;

        await SendEmailAsync(
            emailMessage: emailMessage,
            emailConfiguration: emailConfiguration,
            cancellationToken);
    }

    /// <inheritdoc />
    public async Task SendEmailAsync(
        string to,
        string message,
        string subject,
        CancellationToken cancellationToken)
    {
        var emailConfiguration = _configuration.GetSection("EmailSettings");

        using var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(emailConfiguration["FromName"],
            emailConfiguration["EmailAddress"]));

        emailMessage.To.Add(new MailboxAddress("", to));

        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = message };

        var body = bodyBuilder.ToMessageBody();

        emailMessage.Body = body;

        await SendEmailAsync(
            emailMessage: emailMessage,
            emailConfiguration: emailConfiguration,
            cancellationToken);
    }

    private async Task SendEmailAsync(
        MimeMessage emailMessage,
        IConfigurationSection emailConfiguration,
        CancellationToken cancellationToken)
    {
        using var client = new SmtpClient();

        await client.ConnectAsync(emailConfiguration["SMTPServerHost"],
            int.Parse(emailConfiguration["SMTPServerPort"]!), true, cancellationToken);

        await client.AuthenticateAsync(emailConfiguration["EmailAddress"],
            emailConfiguration["Password"], cancellationToken);

        await client.SendAsync(emailMessage, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}