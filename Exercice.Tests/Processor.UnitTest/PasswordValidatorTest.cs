using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Processor.Services;

namespace Processor.UnitTest;

public class PasswordValidatorTest
{
    [Trait("Category", "Password_Validation")]
    [Theory]
    [InlineData("Password123", false)] // Pas de caractère spéciaux
    [InlineData("Password123*", true)]
    [InlineData("password123*", false)] // Pas de majuscule
    [InlineData("PASSWORD123", false)]  // Pas de minuscule
    [InlineData("Password", false)]     // Pas de chiffre
    [InlineData("Pass12*", false)]      // Moins de 8 caractères 
    [InlineData("Pass123**", true)]
    [InlineData("", false)]             // Vide
    [InlineData("        ", false)]     // 8 Espace vide
    [InlineData("MonSuperMotDePass1*", true)]
    [InlineData("MonSuperMotDePasse123456*", false)] // Plus de 20 caractères
    [InlineData(null, false)]            // Null
    public void Validate_WithDifferentPassword_ReturnsCorrectResult(string password, bool expectedValid)
    {
        // Arrange
        PasswordValidator passwordValidator = new PasswordValidator();
        
        // Act
        Processor.Model.ValidationResult isValid = passwordValidator.Validate(password);
        
        // Assert
        Assert.Equal(expectedValid, isValid.IsValid);
    }

    [Trait("Category", "Password_Validation")]
    [Theory]
    [InlineData("password123", new[] { "Le mot de passe doit contenir au moins une majuscule", "Le mot de passe doit contenir au moins un caractère spécial" })]
    [InlineData("PASSWORD123", new[] { "Le mot de passe doit contenir au moins une minuscule", "Le mot de passe doit contenir au moins un caractère spécial" })]
    [InlineData("Password123", new[] { "Le mot de passe doit contenir au moins un caractère spécial" })]
    [InlineData("Password*", new[] { "Le mot de passe doit contenir au moins un chiffre" })]
    [InlineData("Pass123", new[] { "Le mot de passe doit contenir au moins 8 caractères", "Le mot de passe doit contenir au moins un caractère spécial" })]
    [InlineData("", new[] { "Le mot de passe ne peut pas être vide", "Le mot de passe doit contenir au moins 8 caractères", "Le mot de passe doit contenir au moins une majuscule", "Le mot de passe doit contenir au moins une minuscule", "Le mot de passe doit contenir au moins un chiffre", "Le mot de passe doit contenir au moins un caractère spécial" })]
    [InlineData("        ", new[] { "Le mot de passe ne peut pas être vide", "Le mot de passe doit contenir au moins 8 caractères", "Le mot de passe doit contenir au moins une majuscule", "Le mot de passe doit contenir au moins une minuscule", "Le mot de passe doit contenir au moins un chiffre", "Le mot de passe doit contenir au moins un caractère spécial" })]
    [InlineData(null, new[] { "Le mot de passe ne peut pas être vide", "Le mot de passe doit contenir au moins 8 caractères", "Le mot de passe doit contenir au moins une majuscule", "Le mot de passe doit contenir au moins une minuscule", "Le mot de passe doit contenir au moins un chiffre", "Le mot de passe doit contenir au moins un caractère spécial" })]
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
    [Trait("Category", "Password_Validation")]
    [Theory]
    [MemberData(nameof(PasswordWithErrors))]
    public void Validate_WithErrors_ReturnsExpectedErrorMessages(
        string password, string[] expectedErrorContains)
    {
        // Arrange
        var passwordValidator = new PasswordValidator();

        // Act
        var result = passwordValidator.Validate(password);

        // Assert
        foreach (var expectedError in expectedErrorContains)
        {
            Assert.Contains(result.Errors, error => error.Contains(expectedError));
        }
    }

    public static IEnumerable<object[]> PasswordWithErrors()
    {
        yield return new object[] { "password123", new[] { "majuscule" } };
        yield return new object[] { "PASSWORD123", new[] { "minuscule" } };
        yield return new object[] { "Password123", new[] { "caractère spécial" } };
        yield return new object[] { "Password", new[] { "chiffre" } };
        yield return new object[] { "Pass123", new[] { "8 caractères" } };
        yield return new object[] { "", new[] { "vide", "8 caractères", "majuscule", "minuscule", "chiffre" } };
        yield return new object[] { "Pass", new[] { "8 caractères", "chiffre" } };
        yield return new object[] { "pass123", new[] { "majuscule" } };
        yield return new object[] { " ", new[] { "vide", "8 caractères", "majuscule", "minuscule", "chiffre" } };
        yield return new object[] { null, new[] { "vide", "8 caractères", "majuscule", "minuscule", "chiffre" } };
    }
}