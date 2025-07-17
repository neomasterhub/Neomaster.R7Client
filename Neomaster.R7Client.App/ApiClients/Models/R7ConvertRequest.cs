using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Neomaster.R7Client.Common;

namespace Neomaster.R7Client.App;

/// <summary>
/// Запрос на конвертацию в API.
/// </summary>
public class R7ConvertRequest
{
  /// <summary>
  /// Заголовок JWT для конвертации.
  /// </summary>
  public const string JwtHeaderJson =
    """
    {
      "alg": "HS256",
      "typ": "JWT"
    }
    """;

  /// <summary>
  /// Опции сериализации JSON для конвертации.
  /// </summary>
  public static readonly JsonSerializerOptions JsonOptions;

  static R7ConvertRequest()
  {
    JsonOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
      Converters = { new JsonStringEnumConverter() },
    };
  }

  public R7ConvertRequest(
    string url,
    R7ConvertInputType inputType,
    R7ConvertOutputType outputType,
    string? key = null,
    bool async = false)
  {
    Async = async;
    InputType = inputType;
    OutputType = outputType;
    Key = key ?? Guid.NewGuid().ToString();

    if (string.IsNullOrEmpty(url))
    {
      throw new ArgumentNullException(nameof(url));
    }

    Url = url;
  }

  /// <summary>
  /// При использовании асинхронного типа запроса ответ формируется мгновенно.
  /// В этом случае для получения результата необходимо отправлять запросы без изменения параметров до завершения конвертации.
  /// </summary>
  public bool Async { get; }

  /// <summary>
  /// Уникальный идентификатор файла для сервера документов.
  /// </summary>
  public string Key { get; }

  /// <summary>
  /// Прямая ссылка на исходный файл (static file/download stream).
  /// </summary>
  public string Url { get; }

  /// <summary>
  /// Тип входного файла для конвертации.
  /// </summary>
  [JsonPropertyName("filetype")]
  public R7ConvertInputType InputType { get; }

  /// <summary>
  /// Тип выходного файла для конвертации.
  /// </summary>
  [JsonPropertyName("outputtype")]
  public R7ConvertOutputType OutputType { get; }

  /// <summary>
  /// Создает токен, содержащий параметры конвертации.
  /// </summary>
  /// <param name="secret">Секрет сервера документов.</param>
  /// <returns>Токен, содержащий параметры конвертации.</returns>
  public string ToJwt(string secret)
  {
    if (string.IsNullOrEmpty(secret))
    {
      throw new ArgumentNullException(nameof(secret));
    }

    var headerBase64 = JwtHeaderJson.ToBase64UrlEncoded();
    var payloadBase64 = JsonSerializer.Serialize(this, JsonOptions).ToBase64UrlEncoded();

    var tokenData = $"{headerBase64}.{payloadBase64}";
    var tokenDataBytes = Encoding.UTF8.GetBytes(tokenData);

    var secretBytes = Encoding.UTF8.GetBytes(secret);
    var signatureBase64 = new HMACSHA256(secretBytes).ComputeHash(tokenDataBytes).ToBase64UrlEncoded();

    var jwt = $"{tokenData}.{signatureBase64}";

    return jwt;
  }
}
