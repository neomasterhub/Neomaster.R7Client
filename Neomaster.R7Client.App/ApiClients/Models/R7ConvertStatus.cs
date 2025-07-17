namespace Neomaster.R7Client.App;

/// <summary>
/// Статус конвертации файла.
/// </summary>
public record R7ConvertStatus
{
  /// <summary>
  /// Код ошибки, если конвертация не удалась.
  /// </summary>
  public int Error { get; set; }

  /// <summary>
  /// Процент завершения конвертации.
  /// </summary>
  public int Percent { get; set; }

  /// <summary>
  /// Конвертация завершена или нет.
  /// </summary>
  public bool EndConvert { get; set; }

  /// <summary>
  /// Ссылка для скачивания файла, полученного в результате конвертации.
  /// </summary>
  public string FileUrl { get; set; } = string.Empty;

  /// <summary>
  /// Тип файла, полученного в результате конвертации.
  /// </summary>
  public string FileType { get; set; } = string.Empty;

  /// <summary>
  /// Расширение файла, полученного в результате конвертации.
  /// </summary>
  public string FileExtension => $".{FileType}";
}
