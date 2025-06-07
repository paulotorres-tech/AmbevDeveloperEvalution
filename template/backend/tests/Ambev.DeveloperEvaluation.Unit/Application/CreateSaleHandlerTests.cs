using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;


namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateSaleProfile>();
            });

            _mapper = config.CreateMapper();

            _handler = new CreateSaleHandler(_saleRepositoryMock.Object, _mapper);
        }

        [Fact(DisplayName = "Should create sale successfully and return Id")]
        public async Task Given_ValidCommand_When_Handled_Then_ShouldReturnSaleId()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            _saleRepositoryMock
                .Setup(repo => repo.GetBySaleNumberAsync(command.SaleNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Sale?)null); // No duplicate

            _saleRepositoryMock
                .Setup(repo => repo.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Sale sale, CancellationToken _) =>
                {
                    typeof(Sale).GetProperty("Id")?.SetValue(sale, Guid.NewGuid());
                    return sale;
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreateSaleResult>(result);
            Assert.NotEqual(Guid.Empty, result.Id);
        }

        [Fact(DisplayName = "Should throw exception when sale number already exists")]
        public async Task Given_DuplicateSaleNumber_When_Handled_Then_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            _saleRepositoryMock
                .Setup(repo => repo.GetBySaleNumberAsync(command.SaleNumber, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Sale(command.SaleNumber, command.SaleDate, 1, "Cliente", 1, "Filial"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }

        [Fact(DisplayName = "Should throw ValidationException when command is invalid")]
        public async Task Given_InvalidCommand_When_Handled_Then_ShouldThrowValidationException()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateInvalidCommand();

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
