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
}
