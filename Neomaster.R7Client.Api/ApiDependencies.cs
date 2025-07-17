using Neomaster.R7Client.App;
using Neomaster.R7Client.Infra;

namespace Neomaster.R7Client.Api;

/// <summary>
/// Подключение зависимостей для работы с API "Р7-Офис. Сервер документов".
/// </summary>
internal static class ApiDependencies
{
  /// <summary>
  /// Подключает зависимости для работы с API "Р7-Офис. Сервер документов".
  /// </summary>
  /// <param name="services">Сервисы веб-приложения.</param>
  /// <param name="configuration">Конфигурация веб-приложения.</param>
  /// <returns>Обогащенные сервисы веб-приложения.</returns>
  /// <exception cref="ArgumentNullException">Если не задана соответствующая секция в конфигурационном файле.</exception>
  public static IServiceCollection AddR7Client(this IServiceCollection services, IConfiguration configuration)
  {
    var r7Settings = configuration.GetSection(nameof(R7ApiSettings)).Get<R7ApiSettings>();

    if (r7Settings == null)
    {
      throw new ArgumentNullException(nameof(R7ApiSettings));
    }

    services.Configure<R7ApiSettings>(configuration.GetSection(nameof(R7ApiSettings)));
    services.AddHttpClient<R7ApiClient>(client => { client.BaseAddress = new Uri(r7Settings.ConvertUrl); });
    services.AddScoped<IR7ApiClient, R7ApiClient>();

    return services;
  }
}
