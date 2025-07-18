using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Neomaster.R7Client.App;
using IOFile = System.IO.File;

namespace Neomaster.R7Client.Api;

/// <summary>
/// API для демонстрации работы с клиентом API "Р7-Офис. Сервер документов".
/// </summary>
/// <param name="memoryCache">Буфер между сервисом и ручкой для скачивания исходного файла сервером конвертации.</param>
/// <param name="configuration">Конфигурация веб-приложения.</param>
/// <param name="r7ApiClient">Клиент для работы с API "Р7-Офис. Сервер документов".</param>
[ApiController]
public class DemoController(
  IMemoryCache memoryCache,
  IConfiguration configuration,
  IR7ApiClient r7ApiClient)
  : ControllerBase
{
  /// <summary>
  /// Рабочая директория для конвертации.
  /// Содержит исходные и результирующие файлы конвертации.
  /// </summary>
  private const string _workingDirectoryConfigurationKey = "WorkingDirectory";

  /// <summary>
  /// Конвертирует документ через API "Р7-Офис. Сервер документов".
  /// </summary>
  /// <param name="request">Запрос на конвертацию документа в PDF.</param>
  /// <returns>Файл, полученный при конвертации из сервера конвертации.</returns>
  [HttpPost("convert")]
  public async Task<IActionResult> ConvertAsync(ConvertDocToPdfRequest request)
  {
    ArgumentNullException.ThrowIfNull(request);
    ArgumentNullException.ThrowIfNull(request.FileName);

    var workingDirectory = configuration[_workingDirectoryConfigurationKey]
      ?? throw new ApplicationException($"Не задана рабочая директория для конвертации: ключ конфигурации \"{_workingDirectoryConfigurationKey}\".");

    var filePath = Path.Combine(workingDirectory, request.FileName);
    if (!IOFile.Exists(filePath))
    {
      throw new FileNotFoundException($"Файл не найден: \"{filePath}\".");
    }

    var fileKey = Guid.NewGuid() + Path.GetExtension(request.FileName);
    var fileBytes = await IOFile.ReadAllBytesAsync(filePath);

    memoryCache.Set(fileKey, fileBytes, TimeSpan.FromMinutes(10));

    var downloadUrl = $"{Request.Scheme}://{Request.Host}/download/{fileKey}";
#if DEBUG
    // В режиме отладки сервер конвертации запущен в докере.
    downloadUrl = downloadUrl.Replace("localhost", "host.docker.internal");
#endif
    var r7request = new R7ConvertRequest(
      downloadUrl,
      request.InputType,
      request.OutputType,
      fileKey,
      false,
      request.SpreadsheetLayout);

    var conversionResult = await r7ApiClient.RequestConversionAsync(r7request);
    var convertedFileName = Path.GetFileNameWithoutExtension(request.FileName) + conversionResult.FileExtension;

    return File(conversionResult.FileStream, MediaTypeNames.Application.Octet, convertedFileName);
  }

  /// <summary>
  /// Скачивает файл по ключу из кэша.
  /// </summary>
  /// <param name="fileKey">Ключ файла в кэше.</param>
  /// <returns>Файл из кэша для конвертации.</returns>
  /// <exception cref="FileNotFoundException">Если файл не найден в кэше.</exception>
  [HttpGet("download/{fileKey}")]
  public IActionResult Download(string fileKey)
  {
    if (string.IsNullOrEmpty(fileKey))
    {
      throw new ArgumentNullException(nameof(fileKey));
    }

    if (!memoryCache.TryGetValue(fileKey, out byte[]? fileBytes) || fileBytes == null)
    {
      throw new FileNotFoundException("Файл не найден в кэше.");
    }

    memoryCache.Remove(fileKey);

    return File(fileBytes, MediaTypeNames.Application.Octet, fileKey);
  }
}
