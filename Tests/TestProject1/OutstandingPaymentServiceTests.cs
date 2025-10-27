using System;
using Application.Services;
using Domain.Entities;

namespace Tests
{

public class OutstandingPaymentsServiceTests
{
    private readonly OutstandingPaymentsService _service = new();

    [Fact]
    public void CalculateDueAmount_ShouldAccountForBalance()
    {
        var subscription = new Subscription { Name = "PremiumWebsite", Id=1, PriceInRsd = 1639 };

        // Arrange
        var subscriber = new Subscriber
        {
            FullName = "Mr Tester",
            LastPaymentDate = new DateTime(2024, 12, 29),
            BalanceInRsd = 1200,
            Id = 1,
            Subscription = subscription
        };
        int subscriberCount = 6;

        // Act
        var result = _service.CalculateDueAmount(subscriber, subscription, subscriberCount);

        // Assert
        decimal expectedPerMonth = 1639m / 6;
        decimal totalOwed = expectedPerMonth * 10;
        decimal expectedRemaining = totalOwed - 1200;
        Assert.Equal(expectedRemaining, result, precision: 2);
    }

    [Fact]
    public void CalculateDueAmount_ShouldReturnZero_WhenBalanceCoversAmount()
    {
        var subscription = new Subscription {Name = "PremiumWebsite",PriceInRsd = 1000 };
        var subscriber = new Subscriber
        {
            FullName = "Mr Tester",
            LastPaymentDate = new DateTime(2025, 1, 1),
            BalanceInRsd = 5000,
            Subscription = subscription
        };

        var result = _service.CalculateDueAmount(subscriber, subscription, 2);

        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateDueAmount_ShouldReturnZero_IfNoMonthsDue()
    {
        var subscription = new Subscription {Name="PremiumWebsite", PriceInRsd = 1639 };

        var subscriber = new Subscriber
        {
            FullName = "Aleksa",
            LastPaymentDate = new DateTime(2025, 10, 15),
            BalanceInRsd = 0,
            Subscription = subscription
        };

        var result = _service.CalculateDueAmount(subscriber, subscription, 6);

        Assert.Equal(0, result);
    }
}
}