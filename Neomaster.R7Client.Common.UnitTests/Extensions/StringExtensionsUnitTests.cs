namespace Neomaster.R7Client.Common.UnitTests;

[TestFixture]
public class StringExtensionsUnitTests
{
  [TestCase("", TestName = "Конвертация пустой строки в URL-безопасный base64.")]
  [TestCase(Consts.UrlUnsafeBase64Src, TestName = "Конвертация строки, приводящей к base64 с небезопасными для URL символами, в URL-безопасный base64.")]
  public void ToBase64UrlEncoded_ShouldConvert(string input)
  {
    var actual = input.ToBase64UrlEncoded();

    foreach (var urlUnsafeChar in Consts.UrlUnsafeChars)
    {
      Assert.That(actual, Does.Not.Contain(urlUnsafeChar));
    }
  }

  [TestCase(TestName = "Декодирование строки, приводящей к base64 с небезопасными для URL символами, из URL-безопасного base64.")]
  public void FromBase64UrlEncoded_ShouldConvert()
  {
    var urlEncodedBase64 = Consts.UrlUnsafeBase64Src.ToBase64UrlEncoded();

    var actual = urlEncodedBase64.FromBase64UrlEncoded();

    Assert.That(actual, Is.EqualTo(Consts.UrlUnsafeBase64Src));
  }

  [TestCase(null, TestName = "Декодирование URL-безопасного base64: исключение при декодировании null.")]
  [TestCase("", TestName = "Декодирование URL-безопасного base64: исключение при декодировании пустой строки.")]
  public void FromBase64UrlEncoded_ShouldCheckInputForNullOrEmpty(string? urlSaveBase64)
  {
    Assert.Throws<ArgumentNullException>(() => urlSaveBase64!.FromBase64UrlEncoded());
  }

  [TestCase(TestName = "Декодирование URL-безопасного base64: исключение при декодировании строки с некорректной длиной.")]
  public void FromBase64UrlEncoded_ShouldCheckInputForValidLength()
  {
    Assert.Throws<FormatException>(() => "12345".FromBase64UrlEncoded());
  }
}
