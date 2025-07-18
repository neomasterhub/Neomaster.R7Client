namespace Neomaster.R7Client.App;

/// <summary>
/// Результат конвертации файла через API.
/// </summary>
public record R7ConvertResponse
{
  /// <summary>
  /// Расширение файла, полученного в результате конвертации.
  /// </summary>
  public string FileExtension { get; init; } = string.Empty;

  /// <summary>
  /// Файл, полученный в результате конвертации.
  /// </summary>
  public Stream FileStream { get; init; } = Stream.Null;
}
