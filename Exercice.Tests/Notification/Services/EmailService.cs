using Notification.Contracts;

namespace Notification.Services;

public class EmailService : IEmailService
{
    public Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        if (!IsValidEmail(to)) return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public bool IsValidEmail(string email)
    {
        string _email = email?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(_email))
        {
            return false;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(_email);
            return string.Equals(addr.Address, _email, StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    public void SendWelcomeEmail(string email, string name)
    {
        if (!IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email address");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty");
        }
    }
}