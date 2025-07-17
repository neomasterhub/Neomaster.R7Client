using System.Text;

namespace Neomaster.R7Client.Common.UnitTests.Extensions;

[TestFixture]
public class EnumerableExtensionsUnitTests
{
  [TestCase("", TestName = "Конвертация пустого массива байтов в URL-безопасный base64.")]
  [TestCase(Consts.UrlUnsafeBase64Src, TestName = "Конвертация массива байтов, приводящего к base64 с небезопасными для URL символами, в URL-безопасный base64.")]
  public void ToBase64UrlEncoded_ShouldConvert(string input)
  {
    var inputBytes = Encoding.UTF8.GetBytes(input);

    var actual = inputBytes.ToBase64UrlEncoded();

    foreach (var urlUnsafeChar in Consts.UrlUnsafeChars)
    {
      Assert.That(actual, Does.Not.Contain(urlUnsafeChar));
    }
  }
}
