namespace Neomaster.R7Client.Infra;

/// <summary>
/// Настройки для работы с API "Р7-Офис. Сервер документов".
/// </summary>
public class R7ApiSettings
{
  /// <summary>
  /// URL для конвертации документов.
  /// </summary>
  public string ConvertUrl { get; set; } = string.Empty;

  /// <summary>
  /// Используется в качестве подписи в JWT.
  /// Адрес значения: файл "/etc/r7-office/documentserver/local.json",
  /// параметр services.CoAuthoring.secret.inbox.string.
  /// </summary>
  public string Secret { get; set; } = string.Empty;
}
