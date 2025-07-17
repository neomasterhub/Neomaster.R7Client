using System.Text;

namespace Neomaster.R7Client.Common;

/// <summary>
/// Расширения <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
  /// <summary>
  /// Конвертирует текст в URL-безопасный base64.
  /// </summary>
  /// <param name="text">Конвертируемый текст.</param>
  /// <returns>URL-безопасный base64.</returns>
  public static string ToBase64UrlEncoded(this string text)
  {
    return Encoding.UTF8.GetBytes(text).ToBase64UrlEncoded();
  }
}
