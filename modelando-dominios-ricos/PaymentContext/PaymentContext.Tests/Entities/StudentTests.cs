﻿using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System.Net;
using System.Reflection.Metadata;
using Document = PaymentContext.Domain.ValueObjects.Document;

namespace PaymentContext.Tests.Entities;
[TestClass]
public class StudentTests
{
    private readonly Student _student;
    private readonly Subscription _subscription;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Name _name;
    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("01411375092", EDocumentType.CPF);
        _email = new Email("batman@dc.com");
        _address = new Address("Rua 1", "1234", "Bairro Legal", "Gotham", "SP", "BR", "13400000");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }
    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email);
        _subscription.AddPayment(payment);

        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Invalid);
    }
    [TestMethod]
    public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.Invalid);
    }
    [TestMethod]
    public void ShouldReturnSuccessWhenAddSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email);
        _subscription.AddPayment(payment);

        _student.AddSubscription(_subscription);

        Assert.IsTrue(_student.Valid);
    }
}