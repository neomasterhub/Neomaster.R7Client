using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Neomaster.R7Client.Common;

namespace Neomaster.R7Client.App.UnitTests;

[TestFixture]
public class R7ConvertRequestUnitTests
{
  [TestCase(TestName = "Конвертация запроса в JWT.")]
  public void ToJwt_ShouldConvertToJwt()
  {
    // Arrange
    const string secret = "secret";
    var spreadsheetLayout = new R7ConvertSpreadsheetLayout
    {
      FitToHeight = 1,
      FitToWidth = 1,
      GridLines = true,
      Headings = true,
      IgnorePrintArea = false,
      Scale = 50,
    };
    var request = new R7ConvertRequest(
      "url",
      R7ConvertInputType.Docx,
      R7ConvertOutputType.Pdfa,
      "key",
      false,
      spreadsheetLayout);

    // Act
    var jwt = request.ToJwt(secret);

    // Assert
    Assert.That(jwt, Is.Not.Null.And.Not.Empty);

    // Проверяем структуру JWT.
    var jwtParts = jwt.Split('.');
    Assert.That(jwtParts, Has.Length.EqualTo(3), "Токен должен состоять из 3 частей, разделенных '.'.");

    // Проверяем заголовок JWT.
    var headerJson = jwtParts[0].FromBase64UrlEncoded();
    Assert.That(headerJson, Is.EqualTo(R7ConvertRequest.JwtHeaderJson), "Неправильный заголовок JWT.");

    // Проверяем полезную нагрузку JWT.
    var payloadJson = jwtParts[1].FromBase64UrlEncoded();
    var payload = JsonSerializer.Deserialize<R7ConvertRequest>(payloadJson, R7ConvertRequest.JsonOptions);
    Assert.That(payload, Is.Not.Null, "Не удалось десериализовать полезную нагрузку JWT.");
    Assert.Multiple(() =>
    {
      Assert.That(payload.Url, Is.EqualTo(request.Url));
      Assert.That(payload.InputType, Is.EqualTo(request.InputType));
      Assert.That(payload.OutputType, Is.EqualTo(request.OutputType));
      Assert.That(payload.Async, Is.EqualTo(request.Async));
      Assert.That(payload.Key, Is.EqualTo(request.Key));
      Assert.That(payload.SpreadsheetLayout, Is.Not.Null);
      Assert.That(payload.SpreadsheetLayout!.FitToHeight, Is.EqualTo(spreadsheetLayout.FitToHeight));
      Assert.That(payload.SpreadsheetLayout.FitToWidth, Is.EqualTo(spreadsheetLayout.FitToWidth));
      Assert.That(payload.SpreadsheetLayout.GridLines, Is.EqualTo(spreadsheetLayout.GridLines));
      Assert.That(payload.SpreadsheetLayout.Headings, Is.EqualTo(spreadsheetLayout.Headings));
      Assert.That(payload.SpreadsheetLayout.IgnorePrintArea, Is.EqualTo(spreadsheetLayout.IgnorePrintArea));
      Assert.That(payload.SpreadsheetLayout.Scale, Is.EqualTo(spreadsheetLayout.Scale));
    });

    // Проверяем подпись JWT.
    var secretBytes = Encoding.UTF8.GetBytes(secret);
    var tokenData = $"{jwtParts[0]}.{jwtParts[1]}";
    var tokenDataBytes = Encoding.UTF8.GetBytes(tokenData);
    var expectedSignature = new HMACSHA256(secretBytes).ComputeHash(tokenDataBytes).ToBase64UrlEncoded();
    Assert.That(jwtParts[2], Is.EqualTo(expectedSignature), "Неправильная подпись JWT.");
  }
}
