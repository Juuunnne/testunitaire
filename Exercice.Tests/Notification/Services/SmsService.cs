using Notification.Contracts;

namespace Notification.Services;

public class SmsService : ISmsService
{
    public Task<bool> SendSmsAsync(string phoneNumber, string message)
    {
        if (!IsValidPhoneNumber(phoneNumber)) return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return false;
        }

        return phoneNumber.All(char.IsDigit) && phoneNumber.Length == 10;
    }
}