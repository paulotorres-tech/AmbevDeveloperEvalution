using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Integration.Shared;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Sales
{
    public class SaleRepositoryTests : IntegrationTestBase
    {
        private readonly SaleRepository _repository;

        public SaleRepositoryTests()
        {
            _repository = new SaleRepository(DbContext);
        }

        [Fact(DisplayName = "Should create and retrieve sale by sale number")]
        public async Task Given_ValidSale_When_CreatedAndRetrievedByNumber_Then_ShouldReturnCorrectSale()
        {
            // Arrange
            var sale = new Sale("SALE001", DateTime.UtcNow, 1, "John", 2, "Loja Central");
            sale.AddItem(101, 3, 100m);
            sale.AddItem(102, 5, 25m);

            // Act
            await _repository.CreateAsync(sale);
            var result = await _repository.GetBySaleNumberAsync("SALE001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SALE001", result!.SaleNumber);
            Assert.Equal(2, result.Items.Count);
        }

        [Fact(DisplayName = "Should cancel sale by sale number")]
        public async Task Given_Sale_When_CancelledBySaleNumber_Then_IsCancelledShouldBeTrue()
        {
            // Arrange
            var sale = new Sale("SALE002", DateTime.UtcNow, 1, "Lucas", 3, "Filial B");
            await _repository.CreateAsync(sale);

            // Act
            var cancelled = await _repository.CancelBySaleNumberAsync("SALE002");
            var result = await _repository.GetBySaleNumberAsync("SALE002");

            // Assert
            Assert.True(cancelled);
            Assert.True(result!.IsCancelled);
        }

        [Fact(DisplayName = "Should return false when cancelling unknown sale")]
        public async Task Given_InvalidSaleNumber_When_Cancelled_Then_ShouldReturnFalse()
        {
            // Act
            var cancelled = await _repository.CancelBySaleNumberAsync("NOTFOUND");

            // Assert
            Assert.False(cancelled);
        }
    }
}
