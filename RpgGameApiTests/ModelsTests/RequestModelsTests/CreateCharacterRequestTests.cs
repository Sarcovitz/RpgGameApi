using System.ComponentModel.DataAnnotations;

namespace RpgGameApiTests.ModelsTests.RequestModelsTests;

[TestFixture]
public class CreateCharacterRequestTests
{
    [Test]
    public void CreateCharacterRequest_CorrectModel_SuccessfulValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = "Character",
            Class = CharacterClass.Archer,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.True);
    }
    
    [Test]
    public void CreateCharacterRequest_AllModelPropertiesAreNullOrEmpty_FailedValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = null,
            Class = 0,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void CreateCharacterRequest_NameIsNull_FailedValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = null,
            Class = CharacterClass.Warrior,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void CreateCharacterRequest_NameIsEmpty_FailedValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = string.Empty,
            Class = CharacterClass.Warrior,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void CreateCharacterRequest_NameIsToShort_FailedValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = "Ch",
            Class = CharacterClass.Warrior,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void CreateCharacterRequest_NameIsToLong_FailedValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = "CharacterXXXXXXXXXXXXX",
            Class = CharacterClass.Warrior,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void CreateCharacterRequest_NameHasInvalidCharacters_FailedValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = "CharacterŻĄ**",
            Class = CharacterClass.Warrior,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void CreateCharacterRequest_ClassIsZero_FailedValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = "Character",
            Class = 0,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void CreateCharacterRequest_ClassHasValueOutOfRange_FailedValidation()
    {
        CreateCharacterRequest? model = new()
        {
            Name = "Character",
            Class = (CharacterClass)50,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }    
}
