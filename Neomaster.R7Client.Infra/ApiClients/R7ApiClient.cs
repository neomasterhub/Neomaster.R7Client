using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using Microsoft.Extensions.Options;
using Neomaster.R7Client.App;

namespace Neomaster.R7Client.Infra;

/// <inheritdoc/>
public class R7ApiClient(
  IHttpClientFactory httpClientFactory,
  IOptionsSnapshot<R7ApiSettings> r7ApiSettingsOptionsSnapshot)
  : IR7ApiClient
{
  private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(IR7ApiClient));
  private readonly R7ApiSettings _r7ApiSettings = r7ApiSettingsOptionsSnapshot.Value;

  /// <inheritdoc/>
  public async Task<R7ConvertResponse> RequestConversionAsync(R7ConvertRequest request, CancellationToken cancellationToken = default)
  {
    using var httpRequest = new HttpRequestMessage(HttpMethod.Post, _r7ApiSettings.ConvertUrl);

    // При использовании секрета вся информация передается в JWT с пустым телом запроса.
    var jwt = request.ToJwt(_r7ApiSettings.Secret);
    httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
    httpRequest.Content = new StringContent(string.Empty);
    httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

    // Ответ от сервера всегда с кодом 200 OK.
    var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
    response.EnsureSuccessStatusCode();

    var convertStatus = await response.Content.ReadFromJsonAsync<R7ConvertStatus>(R7ConvertRequest.JsonOptions, cancellationToken)
      ?? throw new Exception("Не удалось получить статус конвертации: ответ от сервера пустой.");

    if (convertStatus.Error != 0
      && Consts.MapConversionErrorCodeToDescription.TryGetValue(convertStatus.Error, out var errorDescription))
    {
      throw new Exception($"Ошибка конвертации: код {convertStatus.Error}: {errorDescription}");
    }

    if (!convertStatus.EndConvert && !request.Async)
    {
      throw new ApplicationException("Конвертация оборвалась на сервере документов.");
    }

    if (string.IsNullOrEmpty(convertStatus.FileUrl))
    {
      throw new ApplicationException("Не удалось получить URL файла конвертации: ответ от сервера пустой.");
    }

    // Получаем результирующий файл.
    var convertedFileResponse = await _httpClient.GetAsync(convertStatus.FileUrl, cancellationToken);
    convertedFileResponse.EnsureSuccessStatusCode();

    var convertedFileBytes = await convertedFileResponse.Content.ReadAsByteArrayAsync(cancellationToken);
    var convertedFileMemoryStream = new MemoryStream(convertedFileBytes);
    convertedFileMemoryStream.Position = 0;

    var result = new R7ConvertResponse
    {
      FileExtension = convertStatus.FileExtension,
      FileStream = convertedFileMemoryStream,
    };

    return result;
  }
}
