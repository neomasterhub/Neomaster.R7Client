using Neomaster.R7Client.App;

namespace Neomaster.R7Client.Api;

/// <summary>
/// Запрос на конвертацию документа в PDF.
/// </summary>
public record ConvertDocToPdfRequest
{
  /// <summary>
  /// Имя файла для конвертации в рабочей директории.
  /// </summary>
  public string FileName { get; init; } = string.Empty;

  /// <summary>
  /// Тип входного файла для конвертации.
  /// </summary>
  public R7ConvertInputType InputType { get; init; }

  /// <summary>
  /// Тип выходного файла для конвертации.
  /// </summary>
  public R7ConvertOutputType OutputType { get; init; }

  /// <summary>
  /// Настройки для конвертации электронной таблицы.
  /// </summary>
  public R7ConvertSpreadsheetLayout? SpreadsheetLayout { get; init; }
}
