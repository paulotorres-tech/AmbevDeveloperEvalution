using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CancelSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly CancelSaleHandler _handler;

        public CancelSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _handler = new CancelSaleHandler(_saleRepositoryMock.Object);
        }

        [Fact(DisplayName = "Should cancel sale by Sale Id successfully.")]
        public async Task Given_ValidSaleId_When_Cancelled_Then_ReturnSuccess()
        {
            // Arrange
            var command = CancelSaleHandlerTestData.ValidByIdCommand();
            _saleRepositoryMock.Setup(r => r.CancelByIdAsync(command.SaleId!.Value, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Sale successfully cancelled.", result.Message);
        }

        [Fact(DisplayName = "Should cancel sale by SaleNumber successfully")]
        public async Task Given_ValidSaleNumber_When_Cancelled_Then_ReturnSuccess()
        {
            // Arrange
            var command = CancelSaleHandlerTestData.ValidBySaleNumberCommand();
            _saleRepositoryMock.Setup(r => r.CancelBySaleNumberAsync(command.SaleNumber!, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Sale successfully cancelled.", result.Message);
        }

        [Fact(DisplayName = "Should return error when neither SaleId nor SaleNumber is provided")]
        public async Task Given_EmptyCommand_When_Cancelled_Then_ReturnError()
        {
            // Arrange
            var command = CancelSaleHandlerTestData.InvalidCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Either SaleId or SaleNumber must be provided.", result.Message);
        }

        [Fact(DisplayName = "Should return error when SaleId not found")]
        public async Task Given_InvalidSaleId_When_Cancelled_Then_ReturnFailure()
        {
            // Arrange
            var saleId = Guid.NewGuid();
            var command = CancelSaleHandlerTestData.InvalidByIdCommand(saleId);

            _saleRepositoryMock.Setup(r => r.CancelByIdAsync(saleId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Sale not found or already cancelled.", result.Message);
        }

        [Fact(DisplayName = "Should return error when SaleNumber not found")]
        public async Task Given_InvalidSaleNumber_When_Cancelled_Then_ReturnFailure()
        {
            // Arrange
            var saleNumber = "SALE404";
            var command = CancelSaleHandlerTestData.InvalidBySaleNumberCommand(saleNumber);

            _saleRepositoryMock.Setup(r => r.CancelBySaleNumberAsync(saleNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Sale not found or already cancelled.", result.Message);
        }
    }
}
