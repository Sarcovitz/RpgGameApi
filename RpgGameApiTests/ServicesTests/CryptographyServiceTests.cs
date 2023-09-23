using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGameApiTests.ServicesTests;

[TestFixture]
public class CryptographyServiceTests
{
    ICryptographyService _cryptographyService;

    [SetUp]
    public void Setup()
    {
        _cryptographyService = new CryptographyService();
    }

    [TestCase("test", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08")]
    [TestCase(" ", "36a9e7f1c95b82ffb99743e0c5c4ce95d83c9a430aac59f84ef3cbfab6145068")]
    [TestCase("ŚĆŹ", "2487af03515f906c052f1e22140ee73d5df036266a3a977a7b4d90412a71dfa5")]
    [TestCase("@!#", "6166a76e338254d025dc0e3b7d23f93e2a66440b997e1db6564ba86c86b2d465")]
    [TestCase("Pa$$w0rd", "97c94ebe5d767a353b77f3c0ce2d429741f2e8c99473c3c150e2faa3d14c9da6")]
    public void Sha256Hash_OnSuccess_ReturnsValidSha256Hash(string text, string expectedHash)
    {
        string result = _cryptographyService.Sha256Hash(text);

        Assert.That(result, Is.EqualTo(expectedHash));
    }
}
