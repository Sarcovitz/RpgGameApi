using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgGameApiTests.ServicesTests;

[TestFixture]
public class InventoryServiceTests
{
    private IInventoryService _inventoryService;

    private Mock<ICharacterRepository> _characterRepositoryMock;
    private Mock<IInventoryRepository> _inventoryRepositoryMock;


    [SetUp]
    public void Setup()
    {
        _characterRepositoryMock = new Mock<ICharacterRepository>();
        _inventoryRepositoryMock = new Mock<IInventoryRepository>();

        _inventoryService = new InventoryService(_inventoryRepositoryMock.Object, _characterRepositoryMock.Object);
    }

    public void SetupInventoryService()
    {
        _inventoryService = new InventoryService(_inventoryRepositoryMock.Object, _characterRepositoryMock.Object);
    }

    [Test]
    public async Task CreateAsync_OnSuccess_ReturnsCreateInventoryDTO()
    {
        ulong userId = 123;
        CreateInventoryRequest request = new()
        {
            CharacterId = 123,            
        };

        Character? foundCharacter = new()
        {
            UserId = userId,
        };

        Inventory? nullInventory = null;
        Inventory? foundInventory = new()
        {
            CharacterId = 123,
            Id = 123
        };

        CreateInventoryDTO expected = new()
        {
            Id = 123,
            CharacterId = 123
        };

        _characterRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<ulong>()))
            .ReturnsAsync(foundCharacter);

        _inventoryRepositoryMock.Setup(x => x.GetByCharacterIdAsync(It.IsAny<ulong>(), It.IsAny<bool>()))
            .ReturnsAsync(nullInventory);
        _inventoryRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Inventory>()))
            .ReturnsAsync(foundInventory);

        SetupInventoryService();

        var result = await _inventoryService.CreateAsync(userId, request);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<CreateInventoryDTO>());
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(expected.Id));
            Assert.That(result.CharacterId, Is.EqualTo(expected.CharacterId));
        });
    }
}
