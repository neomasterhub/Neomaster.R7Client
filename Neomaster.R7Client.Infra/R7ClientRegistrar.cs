using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neomaster.R7Client.App;
using Neomaster.R7Client.Infra;

namespace Neomaster.R7Client;

/// <summary>
/// Регистратор зависимостей клиента.
/// </summary>
public static class R7ClientRegistrar
{
  /// <summary>
  /// Регистрирует зависимости клиента для взаимодействия с API "Р7-Офис. Сервер документов".
  /// <list type="bullet">
  /// <item>
  /// Регистрирует секцию конфигурации <see cref="R7ApiSettings"/>
  /// как <see cref="IOptionsSnapshot{T}"/>.
  /// </item>
  /// <item>
  /// Регистрирует <see cref="HttpClient"/> для обращения к API "Р7-Офис. Сервер документов"
  /// с базовым адресом из <see cref="R7ApiSettings.ConvertUrl"/>.
  /// </item>
  /// </list>
  /// </summary>
  /// <param name="services">Сервисы приложения.</param>
  /// <param name="configuration">Конфигурация приложения.</param>
  /// <returns>Обогащенные сервисы приложения.</returns>
  /// <exception cref="ArgumentNullException">Если в конфигурации отсутствует секция <see cref="R7ApiSettings"/>.</exception>
  public static IServiceCollection AddR7Client(this IServiceCollection services, IConfiguration configuration)
  {
    var r7Settings = configuration.GetSection(nameof(R7ApiSettings)).Get<R7ApiSettings>();

    ArgumentNullException.ThrowIfNull(r7Settings);
    ArgumentNullException.ThrowIfNullOrEmpty(r7Settings.Secret);
    ArgumentNullException.ThrowIfNullOrEmpty(r7Settings.ConvertUrl);

    services.Configure<R7ApiSettings>(configuration.GetSection(nameof(R7ApiSettings)));
    services.AddHttpClient<R7ApiClient>(client => { client.BaseAddress = new Uri(r7Settings.ConvertUrl); });
    services.AddScoped<IR7ApiClient, R7ApiClient>();

    return services;
  }
}
