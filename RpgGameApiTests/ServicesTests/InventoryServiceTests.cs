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
}
