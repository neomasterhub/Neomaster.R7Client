namespace Neomaster.R7Client.Common;

/// <summary>
/// Расширения <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableExtensions
{
  /// <summary>
  /// Конвертирует байты в URL-безопасный base64.
  /// </summary>
  /// <param name="bytes">Конвертируемые байты.</param>
  /// <returns>URL-безопасный base64.</returns>
  public static string ToBase64UrlEncoded(this IEnumerable<byte> bytes)
  {
    return Convert.ToBase64String(bytes.ToArray())
      .TrimEnd('=')
      .Replace('+', '-')
      .Replace('/', '_');
  }
}
