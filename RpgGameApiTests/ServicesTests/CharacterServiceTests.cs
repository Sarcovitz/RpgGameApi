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

    [Test]
    public async Task CreateAsync_OnSuccess_ReturnsCreateCharacterDTO()
    {
        ulong userId = 123;
        CreateCharacterRequest? request = new()
        {
            Class = CharacterClass.Warrior,
            Name = "Character",
        };
        User? user = new();
        List<Character> characterList = new();
        Character? nullCharacter = null;
        Character createdCharacter = new()
        {
            Id = 123,
            Name = "Character"
        };
        CreateCharacterDTO expectedResult = new()
        {
            Id = 123,
            Name = "Character",
        };

        _characterRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(nullCharacter);
        _characterRepositoryMock.Setup(x => x.GetByUserAsync(It.IsAny<ulong>()))
            .ReturnsAsync(characterList);
        _characterRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Character>()))
            .ReturnsAsync(createdCharacter);

        _userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ulong>()))
            .ReturnsAsync(user);

        SetupCharacterService();

        var result = await _characterService.CreateAsync(userId, request);
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<CreateCharacterDTO>());
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(expectedResult.Id));
            Assert.That(result.Name, Is.EqualTo(expectedResult.Name));
        });
    }

    [Test]
    public void CreateAsync_WhenCharacterClassEnumWrong_ThrowsArgumentException()
    {
        ulong userId = 123;
        CreateCharacterRequest? request = new()
        {
            Class = (CharacterClass) 123,
            Name = "Character",
        };

        string expectedMessage = "Supplied character class is wrong.";

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _characterService.CreateAsync(userId, request);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }

    [Test]
    public void CreateAsync_WhenCharacterWithSuppliedNameAlreadyExists_ThrowsArgumentException()
    {
        ulong userId = 123;
        CreateCharacterRequest? request = new()
        {
            Class = CharacterClass.Warrior,
            Name = "Character",
        };

        Character foundCharacter = new();
        string expectedMessage = "Character with supplied name already exists.";

        _characterRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(foundCharacter);

        SetupCharacterService();

        var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _characterService.CreateAsync(userId, request);
        });

        Assert.That(exception.Message, Is.EqualTo(expectedMessage));
    }
}
