using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class SubscriptionHandlerTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        var command = new CreateBoletoSubscriptionCommand
        {
            FirstName = "Bruce",
            LastName = "Wayne",
            Document = "99999999999",
            Email = "hello@balta.io2",
            BarCode = "123456789",
            BoletoNumber = "12345678",
            PaymentNumber = "12345",
            PaidDate = DateTime.Now,
            ExpireDate = DateTime.Now.AddMonths(1),
            Total = 60,
            TotalPaid = 60,
            Payer = "WAYNE CORP",
            PayerDocument = "12345678911",
            PayerDocumentType = EDocumentType.CPF,
            PayerEmail = "batman@dc.com",
            Street = "asdasd",
            Number = "asdasd",
            Neighborhood = "asdasd",
            City = "asdasd",
            State = "asdasd",
            Country = "asdasd",
            ZipCode = "asdasd"
        };
        handler.Handle(command);
        Assert.AreEqual(false, handler.Valid);
    }
}