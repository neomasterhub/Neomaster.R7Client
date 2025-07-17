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
}
