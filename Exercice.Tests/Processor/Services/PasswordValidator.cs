using Processor.Contracts;
using Processor.Model;

namespace Processor.Services;

public class PasswordValidator : IPasswordValidator
{
    public ValidationResult Validate(string password)
    {
        var result = new ValidationResult();
        var _password = password?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(_password))
        {
            result.AddError("Le mot de passe ne peut pas être vide");
        }
        
        if (_password.Length < 8)
            result.AddError("Le mot de passe doit contenir au moins 8 caractères");
            
        if (!_password.Any(char.IsUpper))
            result.AddError("Le mot de passe doit contenir au moins une majuscule");
            
        if (!_password.Any(char.IsLower))
            result.AddError("Le mot de passe doit contenir au moins une minuscule");
            
        if (!_password.Any(char.IsDigit))
            result.AddError("Le mot de passe doit contenir au moins un chiffre");
        if (!_password.Any(c => "!@#$%^&*()_+[]{}|;:,.<>?".Contains(c)))
            result.AddError("Le mot de passe doit contenir au moins un caractère spécial");

        if (_password.Length > 20)
            result.AddError("Le mot de passe ne doit pas dépasser 20 caractères");

        return result;
    }
}