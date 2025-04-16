using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleValidatorTests
    {
        private readonly CreateSaleValidator _validator = new();

        [Fact(DisplayName = "Should validate CreateSaleCommand successfully when valid")]
        public void Given_ValidCommand_When_Validated_Then_ShouldBeValid()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();

            // Act & Assert
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Should fail validation when required fields are missing or invalid")]
        public void Given_InvalidCommand_When_Validated_Then_ShouldHaveErrors()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateInvalidCommand();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
            result.ShouldHaveValidationErrorFor(x => x.CustomerId);
            result.ShouldHaveValidationErrorFor(x => x.CustomerName);
            result.ShouldHaveValidationErrorFor(x => x.BranchId);
            result.ShouldHaveValidationErrorFor(x => x.BranchName);
            result.ShouldHaveValidationErrorFor(x => x.Items);
        }

        [Fact(DisplayName = "Should validate sale items quantity within limit")]
        public void Given_ItemWithExcessiveQuantity_When_Validated_Then_ShouldHaveError()
        {
            // Arrange
            var command = CreateSaleHandlerTestData.GenerateValidCommand();
            command.Items[0].Quantity = 30; // acima do limite

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor("Items[0].Quantity");
        }
    }
}
