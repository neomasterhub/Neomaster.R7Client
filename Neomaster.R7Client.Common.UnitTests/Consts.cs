namespace Neomaster.R7Client.Common.UnitTests;

/// <summary>
/// Содержит постоянные значения, используемые в тестах.
/// </summary>
internal class Consts
{
  /// <summary>
  /// Пример текста, base64 которого содержит все URL-небезопасные символы: '+', '/', '='.
  /// </summary>
  public const string UrlUnsafeBase64Src = ">>>???++//";

  /// <summary>
  /// URL-небезопасные символы.
  /// </summary>
  public static readonly char[] UrlUnsafeChars =
  [
    '+',
    '/',
    '=',
  ];
}
