using System.ComponentModel.DataAnnotations;

namespace RpgGameApiTests.ModelsTests.RequestModelsTests;

[TestFixture]
public class CreateInventoryRequestTests
{
    [Test]
    public void CreateInventoryRequest_CorrectModel_SuccessfulValidation()
    {
        CreateInventoryRequest? model = new()
        {
            CharacterId = 123,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.True);
    }
    
    [Test]
    public void CreateInventoryRequest_AllPropertiesAreNullOrEmpty_FailedValidation()
    {
        CreateInventoryRequest? model = new()
        {
            CharacterId = 0,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void CreateInventoryRequest_CharacterIdIsZero_FailedValidation()
    {
        CreateInventoryRequest? model = new()
        {
            CharacterId = 0,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
}
