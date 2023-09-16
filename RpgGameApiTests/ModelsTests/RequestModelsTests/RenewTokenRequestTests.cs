using System.ComponentModel.DataAnnotations;

namespace RpgGameApiTests.ModelsTests.RequestModelsTests;

[TestFixture]
public class RenewTokenRequestTests
{
    [Test]
    public void RenewTokenRequest_CorrectModel_SuccessfulValidation()
    {
        RenewTokenRequest? model = new()
        {
            UserId = 1,
            Username = "Username"
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.True);
    }

    [Test]
    public void RenewTokenRequest_AllModelPropertiesAreNullOrZero_FailedValidation()
    {
        RenewTokenRequest? model = new()
        {
            UserId = 0,
            Username = null,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RenewTokenRequest_UserIdIsZero_FailedValidation()
    {
        RenewTokenRequest? model = new()
        {
            UserId = 0,
            Username = "Username",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RenewTokenRequest_UsernameIsNull_FailedValidation()
    {
        RenewTokenRequest? model = new()
        {
            UserId = 1,
            Username = null,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RenewTokenRequest_UsernameIsEmpty_FailedValidation()
    {
        RenewTokenRequest? model = new()
        {
            UserId = 1,
            Username = "",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
}
