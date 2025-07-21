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
  public string Left { get; init; } = R7ClientConsts.DocPageMargins.A4.Left;

  /// <summary>
  /// Отступ справа.
  /// Значение по умолчанию: <see cref="R7ClientConsts.DocPageMargins.A4.Right"/>.
  /// </summary>
  public string Right { get; init; } = R7ClientConsts.DocPageMargins.A4.Right;

  /// <summary>
  /// Отступ сверху.
  /// Значение по умолчанию: <see cref="R7ClientConsts.DocPageMargins.A4.Top"/>.
  /// </summary>
  public string Top { get; init; } = R7ClientConsts.DocPageMargins.A4.Top;

  /// <summary>
  /// Отступ снизу.
  /// Значение по умолчанию: <see cref="R7ClientConsts.DocPageMargins.A4.Bottom"/>.
  /// </summary>
  public string Bottom { get; init; } = R7ClientConsts.DocPageMargins.A4.Bottom;
}
