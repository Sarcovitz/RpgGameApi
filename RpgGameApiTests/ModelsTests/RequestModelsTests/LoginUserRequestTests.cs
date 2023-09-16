using System.ComponentModel.DataAnnotations;

namespace RpgGameApiTests.ModelsTests.RequestModelsTests;

[TestFixture]
public class LoginUserRequestTests
{
    [Test]
    public void LoginUserRequest_CorrectModel_SuccessfulValidation()
    {
        LoginUserRequest? model = new()
        {
            Username = "Username",
            Password = "Password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.True);
    }

    [Test]
    public void LoginUserRequest_AllPropertiesNull_FailedValidation()
    {
        LoginUserRequest? model = new()
        {
            Username = null,
            Password = null,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void LoginUserRequest_AllPropertiesEmpty_FailedValidation()
    {
        LoginUserRequest? model = new()
        {
            Username = "",
            Password = "",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void LoginUserRequest_UsernameIsNull_FailedValidation()
    {
        LoginUserRequest? model = new()
        {
            Username = null,
            Password = "Password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void LoginUserRequest_UsernameIsEmpty_FailedValidation()
    {
        LoginUserRequest? validModel = new()
        {
            Username = "",
            Password = "Password",
        };

        var context = new ValidationContext(validModel);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(validModel, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void LoginUserRequest_PasswordIsNull_FailedValidation()
    {
        LoginUserRequest? model = new()
        {
            Username = "Username",
            Password = null,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void LoginUserRequest_PasswordIsEmpty_FailedValidation()
    {
        LoginUserRequest? model = new()
        {
            Username = "Username",
            Password = "",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
}
