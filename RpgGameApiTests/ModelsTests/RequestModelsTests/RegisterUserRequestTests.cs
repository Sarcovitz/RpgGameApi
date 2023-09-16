using System.ComponentModel.DataAnnotations;

namespace RpgGameApiTests.ModelsTests.RequestModelsTests;

[TestFixture]
public class RegisterUserRequestTests
{
    [Test]
    public void RegisterUserRequest_CorrectModel_SuccessfulValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "username",
            Email = "email@mail.com",
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.True);
    }

    [Test]
    public void RegisterUserRequest_AllModelPropertiesAreNull_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = null,
            Email = null,
            Password = null,
            Password2 = null,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RegisterUserRequest_UsernameIsNull_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = null,
            Email = "email@mail.com",
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RegisterUserRequest_UsernameIsEmpty_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = string.Empty,
            Email = "email@mail.com",
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RegisterUserRequest_UsernameHasInvalidCharacters_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "abcĆĄŻ***",
            Email = "email@mail.com",
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RegisterUserRequest_UsernameIsToShort_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Us",
            Email = "email@mail.com",
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RegisterUserRequest_UsernameIsToLong_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "UsernameUsernameUsername",
            Email = "email@mail.com",
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RegisterUserRequest_EmailIsNull_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = null,
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RegisterUserRequest_EmailIsEmpty_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = string.Empty,
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void RegisterUserRequest_EmailIsInvalidEmail_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = "test",
            Password = "password",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void RegisterUserRequest_PasswordIsNull_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = "email@mail.com",
            Password = null,
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void RegisterUserRequest_PasswordIsEmpty_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = "email@mail.com",
            Password = string.Empty,
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void RegisterUserRequest_PasswordIsToShort_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = "email@mail.com",
            Password = "Passs",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    } 
    
    [Test]
    public void RegisterUserRequest_PasswordIsToLong_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = "email@mail.com",
            Password = "PASSSSSSSSSSSSSSSSS",
            Password2 = "password",
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
    
    [Test]
    public void RegisterUserRequest_Password2IsNull_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = "email@mail.com",
            Password = "password",
            Password2 = null,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }

    [Test]
    public void RegisterUserRequest_Password2IsEmpty_FailedValidation()
    {
        RegisterUserRequest? model = new()
        {
            Username = "Username",
            Email = "email@mail.com",
            Password = "password",
            Password2 = string.Empty,
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isModelStateValid = Validator.TryValidateObject(model, context, results, true);
        Assert.That(isModelStateValid, Is.False);
    }
}
