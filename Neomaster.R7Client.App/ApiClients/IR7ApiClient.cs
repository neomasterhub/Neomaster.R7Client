namespace Neomaster.R7Client.App;

/// <summary>
/// Клиент для работы с API "Р7-Офис. Сервер документов".
/// </summary>
public interface IR7ApiClient
{
  /// <summary>
  /// Отправляет запрос на конвертацию.
  /// При использовании асинхронного типа запроса ответ формируется мгновенно.
  /// В этом случае для получения результата необходимо отправлять запросы без изменения параметров до завершения конвертации.
  /// </summary>
  /// <param name="request">Запрос на конвертацию.</param>
  /// <param name="cancellationToken">Токен отмены.</param>
  /// <returns>Статус конвертации.</returns>
  Task RequestConversionAsync(R7ConvertRequest request, CancellationToken cancellationToken = default);
}
