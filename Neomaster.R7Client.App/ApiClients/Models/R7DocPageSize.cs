using System.ComponentModel;

namespace Neomaster.R7Client.App;

/// <summary>
/// Размеры страницы документа.
/// По умолчанию размеры установлены для страницы формата A4.
/// </summary>
public record R7DocPageSize
{
  /// <summary>
  /// Ширина страницы.
  /// Значение по умолчанию: <see cref="R7ClientConsts.DocPageSize.A4.Width"/>.
  /// </summary>
  [DefaultValue(R7ClientConsts.DocPageSize.A4.Width)]
  public string Width { get; init; } = R7ClientConsts.DocPageSize.A4.Width;

  /// <summary>
  /// Высота страницы.
  /// Значение по умолчанию:  <see cref="R7ClientConsts.DocPageSize.A4.Height"/>.
  /// </summary>
  [DefaultValue(R7ClientConsts.DocPageSize.A4.Height)]
  public string Height { get; init; } = R7ClientConsts.DocPageSize.A4.Height;
}
