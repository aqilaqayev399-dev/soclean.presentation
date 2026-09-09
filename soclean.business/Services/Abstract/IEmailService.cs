namespace soclean.business.Services.Abstract;

public interface IEmailService
{
    void SendEmail(string toEmail, string subject, string emailBody);

}