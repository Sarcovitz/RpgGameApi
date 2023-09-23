using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGameApiTests.ServicesTests;

[TestFixture]
public class CharacterServiceTests
{
    private ICharacterService _characterService;

    private Mock<ICharacterRepository> _characterRepositoryMock;
    private Mock<IUserRepository> _userRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _characterRepositoryMock = new Mock<ICharacterRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();

        _characterService = new CharacterService(_characterRepositoryMock.Object, _userRepositoryMock.Object);
    }

    private void SetupCharacterService()
    {
        _characterService = new CharacterService(_characterRepositoryMock.Object, _userRepositoryMock.Object);
    }
}
