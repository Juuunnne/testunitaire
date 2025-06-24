using System.ComponentModel.DataAnnotations;
using Processor.Services;

namespace Processor.UnitTest;

public class PasswordValidatorTest
{
    
    [Theory]
    [InlineData("Password123", true)]
    [InlineData("password123", false)] // Pas de majuscule
    [InlineData("PASSWORD123", false)] // Pas de minuscule
    [InlineData("Password", false)]    // Pas de chiffre
    [InlineData("Pass123", false)]     // Trop court
    [InlineData("", false)]            // Vide
    public void Validate_WithDifferentPassword_ReturnsCorrectResult(string password, bool expectedValid)
    {
        // Arrange
        PasswordValidator passwordValidator = new PasswordValidator();
        
        // Act
        Processor.Model.ValidationResult isValid = passwordValidator.Validate(password);
        
        // Assert
        Assert.Equal(expectedValid, isValid.IsValid);
    }
    
    [Theory]
    [InlineData("password123", new[] { "Le mot de passe doit contenir au moins une majuscule" })]
    [InlineData("PASSWORD123", new[] { "Le mot de passe doit contenir au moins une minuscule" })]
    [InlineData("Password", new[] { "Le mot de passe doit contenir au moins un chiffre" })]
    [InlineData("Pass123", new[] { "Le mot de passe doit contenir au moins 8 caractères" })]
    [InlineData("", new[] { "Le mot de passe ne peut pas être vide", "Le mot de passe doit contenir au moins 8 caractères", "Le mot de passe doit contenir au moins une majuscule", "Le mot de passe doit contenir au moins une minuscule", "Le mot de passe doit contenir au moins un chiffre" })]

    public void Validate_WithError_ReturnCorrectErrorMessage (string password,  string[] expectedErrors)
    {
        // Arrange
        PasswordValidator passwordValidator = new PasswordValidator();
        Processor.Model.ValidationResult expected = new Processor.Model.ValidationResult();
        foreach (var error in expectedErrors) expected.AddError(error);
        
        // Act
        Processor.Model.ValidationResult isValid = passwordValidator.Validate(password);
        
        // Assert
        Assert.Equal(expected.Errors, isValid.Errors);
    }

    /// <summary>
    /// Validates that an invalid password returns the expected set of error messages.
    /// </summary>
    /// <param name="password">The password to be validated.</param>
    /// <param name="expectedErrorContains">An array of expected error messages or substrings that should be included in the validation errors.</param>
    // [Theory]
    // [MemberData(nameof(PasswordWithErrors))]
    public void Validate_MotDePasseInvalide_ContientErreursAttendues(
        string password, string[] expectedErrorContains)
    {
        // Arrange
        
        // Act
        
        // Assert
        foreach (var expectedError in expectedErrorContains)
        {
           
        }
    }


}