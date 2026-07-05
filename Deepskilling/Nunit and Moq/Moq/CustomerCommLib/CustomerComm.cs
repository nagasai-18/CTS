namespace CustomerCommLib;

public class CustomerComm
{
    private readonly IMailSender _mailSender;

    public CustomerComm(IMailSender mailSender)
    {
        _mailSender = mailSender;
    }

    public bool SendMailToCustomer()
    {
        _mailSender.SendMail(
            "cust123@abc.com",
            "Welcome to our website."
        );

        return true;
    }
}