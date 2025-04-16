using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact(DisplayName = "Should create sale with valid data and raise SaleCreatedEvent")]
        public void Given_ValidData_When_CreatingSale_Then_ShouldRaiseSaleCreatedEvent() {
            // Arrange and Act
            var sale = SaleTestData.CreateValidSale();
            

            // Assert
            Assert.NotNull(sale);
            Assert.Single(sale.DomainEvents);
            Assert.IsType<SaleCreatedEvent>(sale.DomainEvents.First());
        }

        [Theory(DisplayName = "Should calculate correct discount based on quantity")]
        [InlineData(3, 0)]     // No discount
        [InlineData(5, 0.10)]  // 10% discount
        [InlineData(12, 0.20)] // 20% discount
        public void Given_ItemQuantity_When_AddedToSale_Then_DiscountShouldBeAppliedCorrectly(int quantity, decimal expectedDiscount)
        {
            // Arrange
            var sale = SaleTestData.CreateValidSale();
            var productId = SaleTestData.GenerateProductId();
            var unitPrice = 100.0m;

            // Act
            sale.AddItem(productId, quantity, unitPrice);
            var item = sale.Items.First();

            // Assert
            Assert.Equal(expectedDiscount, item.DiscountPercent);
        }

        [Fact(DisplayName = "Should throw exception when adding more than 20 identical items")]
        public void Given_ExcessiveQuantity_When_AddingToSale_Then_ShouldThrowException()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSale();
            var productId = SaleTestData.GenerateProductId();
            var unitPrice = 100.0m;

            // Act & Assert
            Assert.Throws<DomainException>(() =>
                sale.AddItem(productId, quantity: 25, unitPrice));
        }

        [Fact(DisplayName = "Should cancel sale and raise SaleCancelledEvent")]
        public void Given_ValidSale_When_Cancelled_Then_ShouldSetIsCancelledAndRaiseEvent()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSale();

            // Act
            sale.CancelSale();

            // Assert
            Assert.True(sale.IsCancelled);
            Assert.Contains(sale.DomainEvents, e => e is SaleCancelledEvent);
        }

        [Fact(DisplayName = "Should throw exception when cancelling sale twice")]
        public void Given_AlreadyCancelledSale_When_CancelledAgain_Then_ShouldThrowException()
        {
            // Arrange
            var sale = SaleTestData.CreateValidSale();
            sale.CancelSale();

            // Act & Assert
            Assert.Throws<DomainException>(() => sale.CancelSale());
        }
    }
}
