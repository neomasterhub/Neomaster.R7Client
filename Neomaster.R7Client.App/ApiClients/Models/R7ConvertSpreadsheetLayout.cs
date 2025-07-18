namespace Neomaster.R7Client.App;

/// <summary>
/// Настройки для конвертации электронной таблицы.
/// </summary>
public record R7ConvertSpreadsheetLayout
{
  /// <summary>
  /// Размер страницы результирующего файла.
  /// </summary>
  public R7DocPageSize PageSize { get; init; } = new();

  /// <summary>
  /// Внешние отступы страницы результирующего файла.
  /// </summary>
  public R7DocPageMargins Margins { get; init; } = new();

  /// <summary>
  /// Ориентация страницы результирующего файла.
  /// </summary>
  public R7DocPageOrientation Orientation { get; init; }

  /// <summary>
  /// Масштаб результирующего файла в процентах.
  /// Значение по умолчанию: 100.
  /// </summary>
  public int Scale { get; init; } = 100;

  /// <summary>
  /// Определяет, следует ли игнорировать область печати, выбранную для файла электронной таблицы.
  /// Значение по умолчанию: true.
  /// </summary>
  public bool IgnorePrintArea { get; init; } = true;

  /// <summary>
  /// Позволяет включать заголовки в результирующий файл.
  /// Значение по умолчанию: false.
  /// </summary>
  public bool Headings { get; init; }

  /// <summary>
  /// Позволяет включать линии сетки в результирующий файл.
  /// Значение по умолчанию: false.
  /// </summary>
  public bool GridLines { get; init; }

  /// <summary>
  /// Задает высоту преобразованной области в результирующем файле, измеряемую в количестве страниц.
  /// Значение по умолчанию: 0.
  /// </summary>
  public int FitToHeight { get; init; }

  /// <summary>
  /// Задает ширину преобразованной области в результирующем файле, измеряемую в количестве страниц.
  /// Значение по умолчанию: 0.
  /// </summary>
  public int FitToWidth { get; init; }
}
