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

  /// <summary>
  /// Декодирует URL-безопасный base64 в текст.
  /// </summary>
  /// <param name="urlEncodedBase64">URL-безопасный base64.</param>
  /// <returns>Текст, закодированный в URL-безопасный base64.</returns>
  /// <exception cref="ArgumentNullException">Если входное значение null или пустое.</exception>
  /// <exception cref="FormatException">Если входное значение некорректно.</exception>
  public static string FromBase64UrlEncoded(this string urlEncodedBase64)
  {
    if (string.IsNullOrEmpty(urlEncodedBase64))
    {
      throw new ArgumentNullException(nameof(urlEncodedBase64), "URL-безопасный base64 не может быть null или пустым.");
    }

    if (urlEncodedBase64.Length % 4 == 1)
    {
      throw new FormatException("URL-безопасный base64 некорректный, т.к. содержит только 1 символ.");
    }

    var base64 = urlEncodedBase64
      .Replace('-', '+')
      .Replace('_', '/');

    switch (base64.Length % 4)
    {
      case 2: base64 += "=="; break;
      case 3: base64 += "="; break;
    }

    return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
  }
}
