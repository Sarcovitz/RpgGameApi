using Microsoft.AspNetCore.Mvc.ModelBinding;
using RpgGame.Misc;
using System.Security.Claims;

namespace RpgGameApiTests.ExtensionsTests;

[TestFixture]
public class ExtensionsTests
{
    [Test]
    public void GetId_WhenIdClaimExists_ShouldReturnValidId()
    {
        var idClaimValue = "123456";
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Id", idClaimValue)
        }));

        var result = claimsPrincipal.GetId();

        Assert.That(result, Is.EqualTo(123456UL));
    }

    [Test]
    public void GetId_WhenIdClaimIsMissing_ShouldReturnZero()
    {
        var claimsPrincipal = new ClaimsPrincipal();

        var result = claimsPrincipal.GetId();

        Assert.That(result, Is.Zero);
    }

    [Test]
    public void GetId_WhenClaimIsWrongFormat_ShouldThrowException()
    {
        var idClaimValue = "abcd";
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim("Id", idClaimValue)
        }));

        Assert.That(claimsPrincipal.GetId, Throws.Exception);
    }

    [Test]
    public void GetErrors_NoErrors_ReturnsEmptyString()
    {
        // Arrange
        ModelStateDictionary modelState = new ModelStateDictionary();

        // Act
        string errors = modelState.GetErrors();

        // Assert
        Assert.That(errors, Is.EqualTo(""));
    }

    [Test]
    public void GetErrors_SingleError_ReturnsSingleErrorMessage()
    {
        // Arrange
        ModelStateDictionary modelState = new ModelStateDictionary();
        modelState.AddModelError("fieldName", "This field is required");

        // Act
        string errors = modelState.GetErrors();

        // Assert
        Assert.That(errors, Is.EqualTo($"This field is required"));
    }

    [Test]
    public void GetErrors_MultipleErrors_ReturnsConcatenatedErrorMessages()
    {
        // Arrange
        ModelStateDictionary modelState = new ModelStateDictionary();
        modelState.AddModelError("field1", "Error 1");
        modelState.AddModelError("field2", "Error 2");
        modelState.AddModelError("field3", "Error 3");

        // Act
        string errors = modelState.GetErrors();

        // Assert
        string expectedErrors = $"Error 1{Environment.NewLine}Error 2{Environment.NewLine}Error 3";
        Assert.That(errors, Is.EqualTo(expectedErrors));
    }
}
