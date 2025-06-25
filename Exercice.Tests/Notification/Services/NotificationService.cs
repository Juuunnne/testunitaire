using Notification.Contracts;

namespace Notification.Services;

public class NotificationService(IEmailService emailService, ISmsService smsService, ILogger logger)
    : INotificationService
{

    public async Task<bool> SendWelcomeEmailAsync(string email, string userName)
    {
        if (!emailService.IsValidEmail(email))
        {
            logger.LogWarning($"Invalid email format: {email}");
            return false;
        }
        
        var subject = "Welcome!";
        var body = $"Welcome {userName}! Thank you for joining us.";
        
        try
        {
            var result = await emailService.SendEmailAsync(email, subject, body);
            if (result)
            {
                logger.LogInfo($"Welcome email sent successfully to {email}");
                return true;
            }
            else
            {
                logger.LogError($"Failed to send welcome email to {email}");
                return false;
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"Exception while sending email to {email}: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> SendNotificationAsync(string email, string phoneNumber, string message)
    {
        bool emailSent = false;
        bool smsSent = false;
        
        if (!string.IsNullOrEmpty(email) && emailService.IsValidEmail(email))
        {
            emailSent = await emailService.SendEmailAsync(email, "Notification", message);
            if (emailSent)
            {
                logger.LogInfo($"Email sent successfully to {email}");
            }
            else
            {
                logger.LogError($"Failed to send email to {email}");
            }
        }
        else if (!string.IsNullOrEmpty(email))
        {
            logger.LogWarning($"Invalid email format: {email}");
        }

        if (!string.IsNullOrEmpty(phoneNumber) && smsService.IsValidPhoneNumber(phoneNumber))
        {
            smsSent = await smsService.SendSmsAsync(phoneNumber, message);
            if (smsSent)
            {
                logger.LogInfo($"SMS sent successfully to {phoneNumber}");
            }
            else
            {
                logger.LogError($"Failed to send SMS to {phoneNumber}");
            }
        }
        else if (!string.IsNullOrEmpty(phoneNumber))
        {
            logger.LogWarning($"Invalid phone number format: {phoneNumber}");
        }

        return emailSent || smsSent;
    }
}