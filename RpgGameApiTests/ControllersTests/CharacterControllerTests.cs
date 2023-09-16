using Microsoft.AspNetCore.Mvc;
using RpgGame.Misc;

namespace RpgGameApiTests.ControllersTests;

[TestFixture]
public class CharacterControllerTests
{
    private CharacterController _characterController;

    [SetUp]
    public void Setup()
    {
        var characterServiceMock = new Mock<ICharacterService>();
        characterServiceMock.Setup(x => x.CreateAsync(It.IsAny<ulong>(), It.IsAny<CreateCharacterRequest>()))
            .ReturnsAsync(new CreateCharacterDTO());
        characterServiceMock.Setup(x => x.DeleteAsync(It.IsAny<ulong>()))
            .ReturnsAsync(new SuccessDTO());
        characterServiceMock.Setup(x => x.GetByIdAsync(It.IsAny<ulong>()))
            .ReturnsAsync(new Character());
        characterServiceMock.Setup(x => x.GetAllAsync(It.IsAny<ulong>()))
            .ReturnsAsync(new List<Character>());

        var inventoryServiceMock = new Mock<IInventoryService>();
        inventoryServiceMock.Setup(x => x.GetInventoryByCharacterIdAsync(It.IsAny<ulong>()))
            .ReturnsAsync(new Inventory());

        _characterController = new CharacterController(characterServiceMock.Object, inventoryServiceMock.Object);
    }

    [Test]
    public async Task CreateAsync_ModelIsValid_ReturnsOk()
    {
        CreateCharacterRequest? validModel = new()
        {
            Class = CharacterClass.Warrior,
            Name = "Character"
        };

        var result = await _characterController.CreateAsync(validModel);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
    
    [Test]
    public async Task CreateAsync_ModelIsNull_ReturnsBadRequest()
    {
        CreateCharacterRequest? model = null;

        var result = await _characterController.CreateAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task CreateAsync_ModelIsNull_ReturnsBadRequestMessage()
    {
        CreateCharacterRequest? model = null;

        string expectedMessage = "Model cannot be null.";

        var result = await _characterController.CreateAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task CreateAsync_ModelStateWithErrors_ReturnsBadRequest()
    {
        _characterController.ModelState.AddModelError("Error", "Error Message");

        CreateCharacterRequest? model = new();

        var result = await _characterController.CreateAsync(model);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task CreateAsync_ModelStateWithErrors_ReturnsBadRequestMessage()
    {
        _characterController.ModelState.AddModelError("Error", "Error Message");

        CreateCharacterRequest? model = new();

        string expectedMessage = "Error Message";

        var result = await _characterController.CreateAsync(model);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public async Task DeleteAsync_ValidData_ReturnsOk()
    {
        ulong? id = 123;

        var result = await _characterController.DeleteAsync(id);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task DeleteAsync_IdIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = null;

        var result = await _characterController.DeleteAsync(id);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        id = 0;
        
        result = await _characterController.DeleteAsync(id);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task DeleteAsync_IdIsNullOrEmpty_ReturnsBadRequestMessage()
    {
        ulong? id = null;

        string expectedMessage = "Character id can't be null or empty.";

        var result = await _characterController.DeleteAsync(id);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        id = 0;

        result = await _characterController.DeleteAsync(id);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }
    
    [Test]
    public async Task GetAsync_ValidData_ReturnsOk()
    {
        ulong? id = 123;

        var result = await _characterController.GetAsync(id);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
    
    [Test]
    public async Task GetAsync_IdIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = null;

        var result = await _characterController.GetAsync(id);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        id = 0;

        result = await _characterController.GetAsync(id);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }
    
    [Test]
    public async Task GetAsync_IdIsNullOrEmpty_ReturnsBadRequestMessage()
    {
        ulong? id = null;

        string expectedMessage = "Character id can't be null or empty.";

        var result = await _characterController.GetAsync(id);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        id = 0;

        result = await _characterController.GetAsync(id);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }

    [Test]
    public async Task GetAllAsync_ValidData_ReturnsOk()
    {
        var result = await _characterController.GetAllAsync();
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
    
    [Test]
    public async Task GetCharacterInventoryAsync_ValidData_ReturnsOk()
    {
        ulong? id = 123;

        var result = await _characterController.GetCharacterInventoryAsync(id);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GetCharacterInventoryAsync_IdIsNullOrEmpty_ReturnsBadRequest()
    {
        ulong? id = null;

        var result = await _characterController.GetCharacterInventoryAsync(id);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());

        id = 0;

        result = await _characterController.GetCharacterInventoryAsync(id);
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task GetCharacterInventoryAsync_IdIsNullOrEmpty_ReturnsBadRequestMessage()
    {
        ulong? id = null;

        string expectedMessage = "Character id can't be null or empty.";

        var result = await _characterController.GetCharacterInventoryAsync(id);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));

        id = 0;

        result = await _characterController.GetCharacterInventoryAsync(id);
        Assert.That((result as BadRequestObjectResult)?.Value, Is.EqualTo(expectedMessage));
    }
}
