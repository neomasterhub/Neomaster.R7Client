namespace Neomaster.R7Client.App;

/// <summary>
/// Статус конвертации файла.
/// </summary>
public record R7ConvertStatus
{
  /// <summary>
  /// Код ошибки, если конвертация не удалась.
  /// </summary>
  public int Error { get; init; }

  /// <summary>
  /// Процент завершения конвертации.
  /// </summary>
  public int Percent { get; init; }

  /// <summary>
  /// Конвертация завершена или нет.
  /// </summary>
  public bool EndConvert { get; init; }

  /// <summary>
  /// Ссылка для скачивания файла, полученного в результате конвертации.
  /// </summary>
  public string FileUrl { get; init; } = string.Empty;

  /// <summary>
  /// Тип файла, полученного в результате конвертации.
  /// </summary>
  public string FileType { get; init; } = string.Empty;

  /// <summary>
  /// Расширение файла, полученного в результате конвертации.
  /// </summary>
  public string FileExtension => $".{FileType}";
}
