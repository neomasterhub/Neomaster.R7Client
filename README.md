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

## 🔍 Примеры запросов

### Конвертация Word

```json
{
  "fileName": "docx-01.docx",
  "inputType": "Docx",
  "outputType": "Pdfa"
}
```

### Конвертация Excel

* Лист формата А4 альбомной ориентации.
* Ужимка контента по ширине страницы.
```json
{
  "fileName": "xlsx-01.xlsx",
  "inputType": "Xlsx",
  "outputType": "Pdfa",
  "spreadsheetLayout": {
    "pageSize": {
      "width": "29.7cm",
      "height": "21cm"
    },
    "margins": {
      "left": "1cm",
      "right": "1cm",
      "top": "1cm",
      "bottom": "1cm"
    },
    "fitToWidth": 1
  }
}
```

## 📅 Дорожная карта

- [x] Регистратор зависимостей.
- [ ] Юнит-тесты инфраструктуры.
- [ ] Сравнение с другими способами конвертации.
- [ ] Примеры исходных файлов и запросов в формате JSON для конвертации.
- [ ] Буфер временного хранения исходных файлов для скачивания сервером конвертации.
      Реализация по умолчанию - memory cache.
- [ ] Асинхронная конвертация больших файлов.
- [ ] Настройка таймаутов и повторных попыток запроса на конвертацию.
- [ ] Добавить остальные параметры запроса на конвертацию.

## ℹ️ Примечание

- 🌐 В комплекте с клиентом есть демонстрационное приложение - **Neomaster.R7Client.Api**.
- 📖 [Документация по конвертации](https://support.r7-office.ru/document_server/api-document_server/more_api/conversion-api/)
