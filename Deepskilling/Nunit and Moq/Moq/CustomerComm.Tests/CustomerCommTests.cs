using CustomerCommLib;
using Moq;
using NUnit.Framework;

namespace CustomerComm.Tests;

[TestFixture]
public class CustomerCommTests
{
private Mock<IMailSender> _mailSenderMock;
private CustomerCommLib.CustomerComm _customerComm;

[OneTimeSetUp]
public void Init()
{
_mailSenderMock = new Mock<IMailSender>();

_mailSenderMock
    .Setup(x => x.SendMail(It.IsAny<string>(), It.IsAny<string>()))
    .Returns(true);

    _customerComm = new CustomerCommLib.CustomerComm(_mailSenderMock.Object);
}

[Test]
public void SendMail_Should_Return_True()
{
bool result = _customerComm.SendMailToCustomer();

Assert.That(result, Is.True);
}
}