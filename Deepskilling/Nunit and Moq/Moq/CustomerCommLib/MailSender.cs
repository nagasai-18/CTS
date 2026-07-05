namespace CustomerCommLib;

public class MailSender : IMailSender
{
    public bool SendMail(string toAddress, string message)
    {
        return true;
    }
}