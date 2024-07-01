using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects;

[TestClass]
public class DocumentTests
{
    // Red, Green, Refactor
    [TestMethod]
    public void ShouldReturnErrorWhenCNPJIsInvalid()
    {
        var doc = new Document("123", EDocumentType.CNPJ);
        Assert.IsTrue(doc.Invalid);
    }
    [TestMethod]
    public void ShouldReturnSuccessWhenCNPJIsValid()
    {
        var doc = new Document("73312459000102", EDocumentType.CNPJ);
        Assert.IsTrue(doc.Valid);
    }
    [TestMethod]
    public void ShouldReturnSucessWhenCPFIsInvalid()
    {
        var doc = new Document("123", EDocumentType.CPF);
        Assert.IsTrue(doc.Invalid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("33806141061")]
    [DataRow("12345678912")]
    [DataRow("32165498712")]
    public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
    {
        var doc = new Document(cpf, EDocumentType.CPF);
        Assert.IsTrue(doc.Valid);
    }
}