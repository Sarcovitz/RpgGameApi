using Microsoft.AspNetCore.Mvc;

namespace RpgGameApiTests.ControllersTests;

[TestFixture]
public class InventoryControllerTests
{
    private InventoryController _inventoryController;

    [SetUp]
    public void Setup()
    {
        var inventoryServiceMock = new Mock<IInventoryService>();
        inventoryServiceMock.Setup(x => x.CreateAsync(It.IsAny<ulong>(), It.IsAny<CreateInventoryRequest>()))
            .ReturnsAsync(new CreateInventoryDTO());
        inventoryServiceMock.Setup(x => x.GetInventoryByIdAsync(It.IsAny<ulong>()))
            .ReturnsAsync(new Inventory());

        _inventoryController = new InventoryController(inventoryServiceMock.Object);
    }

    [Test]
    public async Task CreateAsync_ValidData_ReturnsOk()
    {
        CreateInventoryRequest? validModel = new()
        {
            CharacterId = 123,
        };  

        var result = await _inventoryController.CreateAsync(validModel);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task CreateAsync_ModelIsNull_ReturnsBadRequest()
    {
        CreateInventoryRequest? model = null;

        var result = await _inventoryController.CreateAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task CreateAsync_ModelIsNull_ReturnsBadRequestMessage()
    {
        CreateInventoryRequest? model = null;

        string expectedMessage = "Model cannot be null.";

        var result = await _inventoryController.CreateAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public async Task CreateAsync_ModelStateHasErrors_ReturnsBadRequest()
    {
        CreateInventoryRequest? model = new();

        _inventoryController.ModelState.AddModelError("Error", "Error Message");

        var result = await _inventoryController.CreateAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task CreateAsync_ModelStateHasErrors_ReturnsBadRequestMessage()
    {
        CreateInventoryRequest? model = new();

        _inventoryController.ModelState.AddModelError("Error", "Error Message");

        string expectedMessage = "Error Message";

        var result = await _inventoryController.CreateAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task GetAsync_ValidData_ReturnsOk()
    {
        ulong? id = 123;

        var result = await _inventoryController.GetAsync(id);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
    
    [Test]
    public async Task GetAsync_IdIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = null;

        var result = await _inventoryController.GetAsync(id);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        id = 0;

        result = await _inventoryController.GetAsync(id);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task GetAsync_IdIsNullOrEmpty_ReturnsBadRequestMessage()
    {
        ulong? id = null;

        string expectedMessage = "Inventory id can't be null or empty.";

        var result = await _inventoryController.GetAsync(0);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        id = 0;

        result = await _inventoryController.GetAsync(null);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }
}
