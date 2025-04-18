#region

using System.Reflection;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Extensions;

#endregion

namespace RussianSpotify.API.Core.Models;

/// <summary>
///     Хелпер по работе с уведмолениями
/// </summary>
public static class EmailTemplateHelper
{
    private const string TemplatePath = "RussianSpotify.API.Core.Templates.HTML";

    /// <summary>
    ///     Создать уведмоление
    /// </summary>
    /// <param name="placeholders">Шаблоны</param>
    /// <param name="template">Шаблон письма</param>
    /// <param name="head">Заголовок</param>
    /// <param name="emailTo">Кому</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Уведомление</returns>
    public static async Task<EmailNotification> GetEmailNotificationAsync(
        Dictionary<string, string> placeholders,
        string template,
        string head,
        string emailTo,
        CancellationToken cancellationToken)
    {
        var content = await GetEmailTemplateAsync(template, cancellationToken);

        content = content.ReplacePlaceholders(placeholders);

        return EmailNotification.CreateNotification(
            body: content,
            head: head,
            emailTo: emailTo);
    }

    /// <summary>
    ///     Возвращает html шаблон
    /// </summary>
    /// <param name="template">имя html шаблона</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Считанный из файла html шаблон</returns>
    public static async Task<string> GetEmailTemplateAsync(string template, CancellationToken cancellationToken)
    {
        await using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"{TemplatePath}.{template}")
                                 ?? throw new FileNotFoundException(
                                     $"Шаблон Email сообщения с названием {template} не найден");
        using var reader = new StreamReader(stream);

        return await reader.ReadToEndAsync(cancellationToken);
    }
}