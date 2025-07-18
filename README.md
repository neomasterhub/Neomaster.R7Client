# R7 Client
Клиент для работы с API "Р7-Офис. Сервер документов".

## 🚀 Возможности

- 📄 **Конвертация в PDF:**
  - Форматы исходных файлов: `doc`, `docx`, `xls`, `xlsx`.
  - Конвертация картинок, колонтитулов, номеров страниц и формул.
  - Настройка масштаба и подгонка под печатные форматы листа.

- 🔐 **Авторизация:**
  - Преобразование запроса в JWT, подписанный HMAC-секретом.
  - Тело запроса отсутствует - все параметры передаются внутри токена.

## 📦 Установка

1. Установить ["Р7-Офис. Сервер документов"](https://support.r7-office.ru/document_server/install-document_server/ds_docker/docker_install_ds/).

2. Добавить [NuGet-пакет](https://www.nuget.org/packages/Neomaster.R7Client).

```bash
dotnet add package Neomaster.R7Client
```
3. Добавить секцию в `appsettings.json`:

```json
"R7ApiSettings": {
  "ConvertUrl": "http://localhost:59001/ConvertService.ashx",
  "Secret": "eGzvoLPMEIJnGYEBIb30N8GvodOBnoJF"
},
"WorkingDirectory": "D:/r7/conversion"
```
- Значение `Secret` задано в параметре `services.CoAuthoring.secret.inbox.string`
  внутри контейнера в файле `/etc/r7-office/documentserver/local.json`.
- `WorkingDirectory` - рабочая директория с исходными файлами для конвертации.

4. Зарегистрировать зависимости.
```csharp
builder.Services.AddR7Client(builder.Configuration);
builder.Services.AddMemoryCache(); // Буфер временного хранения исходных файлов для скачивания сервером документов.
```

## 📅 Дорожная карта

- [x] Регистратор зависимостей.
- [ ] Буфер временного хранения исходных файлов для скачивания сервером конвертации.
      Реализация по умолчанию - memory cache.
- [ ] Асинхронная конвертация больших файлов.
- [ ] Настройка таймаутов и повторных попыток запроса на конвертацию.

## ℹ️ Примечание

- 🌐 В комплекте с клиентом есть демонстрационное приложение - **Neomaster.R7Client.Api**.
- 📖 [Документация по конвертации](https://support.r7-office.ru/document_server/api-document_server/more_api/conversion-api/)
