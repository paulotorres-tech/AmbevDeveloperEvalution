using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class GetSaleByNumberHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetSaleByNumberHandler _handler;

        public GetSaleByNumberHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GetSaleByNumberProfile>();
            });

            _mapper = config.CreateMapper();
            _handler = new GetSaleByNumberHandler(_saleRepositoryMock.Object, _mapper);
        }

        [Fact(DisplayName = "Should return sale when SaleNumber exists")]
        public async Task Given_ExistingSaleNumber_When_Queried_Then_ShouldReturnSale()
        {
            // Arrange
            var query = GetSaleByNumberHandlerTestData.ValidQuery();
            var sale = GetSaleByNumberHandlerTestData.GenerateValidSale();

            _saleRepositoryMock.Setup(r => r.GetBySaleNumberAsync(query.SaleNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync(sale);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(query.SaleNumber, result.SaleNumber);
            Assert.Equal(sale.TotalAmount, result.TotalAmount);
            Assert.Equal(2, result.Items.Count);
        }

        [Fact(DisplayName = "Should throw KeyNotFoundException when sale not found")]
        public async Task Given_InvalidSaleNumber_When_Queried_Then_ShouldThrowException()
        {
            // Arrange
            var query = GetSaleByNumberHandlerTestData.InvalidQuery();

            _saleRepositoryMock.Setup(r => r.GetBySaleNumberAsync(query.SaleNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Sale?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None));
        }
    }
}
