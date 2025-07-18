using System.ComponentModel;

namespace Neomaster.R7Client.App;

/// <summary>
/// Внешние отступы страницы документа.
/// По умолчанию отступы установлены для страницы формата A4.
/// </summary>
public record R7DocPageMargins
{
  /// <summary>
  /// Отступ слева.
  /// Значение по умолчанию: <see cref="R7ClientConsts.DocPageMargins.A4.Left"/>.
  /// </summary>
  [DefaultValue(R7ClientConsts.DocPageMargins.A4.Left)]
  public string Left { get; init; } = R7ClientConsts.DocPageMargins.A4.Left;

  /// <summary>
  /// Отступ справа.
  /// Значение по умолчанию: <see cref="R7ClientConsts.DocPageMargins.A4.Right"/>.
  /// </summary>
  [DefaultValue(R7ClientConsts.DocPageMargins.A4.Right)]
  public string Right { get; init; } = R7ClientConsts.DocPageMargins.A4.Right;

  /// <summary>
  /// Отступ сверху.
  /// Значение по умолчанию: <see cref="R7ClientConsts.DocPageMargins.A4.Top"/>.
  /// </summary>
  [DefaultValue(R7ClientConsts.DocPageMargins.A4.Top)]
  public string Top { get; init; } = R7ClientConsts.DocPageMargins.A4.Top;

  /// <summary>
  /// Отступ снизу.
  /// Значение по умолчанию: <see cref="R7ClientConsts.DocPageMargins.A4.Bottom"/>.
  /// </summary>
  [DefaultValue(R7ClientConsts.DocPageMargins.A4.Bottom)]
  public string Bottom { get; init; } = R7ClientConsts.DocPageMargins.A4.Bottom;
}
