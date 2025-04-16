using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CreateSaleHandlerTestData
    {
        private static readonly Faker _faker = new();

        public static CreateSaleCommand GenerateValidCommand(int itemCount = 3)
        {
            return new CreateSaleCommand
            {
                SaleNumber = _faker.Random.AlphaNumeric(10).ToUpper(),
                SaleDate = DateTime.UtcNow,
                CustomerId = _faker.Random.Int(1, 1000),
                CustomerName = _faker.Name.FullName(),
                BranchId = _faker.Random.Int(1, 50),
                BranchName = $"Company Name {_faker.Address.City()}",
                Items = GenerateValidItems(itemCount)
            };
        }

        public static List<SaleItemDto> GenerateValidItems(int count)
        {
            return new Faker<SaleItemDto>()
                .RuleFor(i => i.ProductId, f => f.Random.Int(1, 1000))
                .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
                .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(10, 100))
                .Generate(count);
        }

        public static CreateSaleCommand GenerateInvalidCommand()
        {
            return new CreateSaleCommand
            {
                SaleNumber = "", // Invalid
                SaleDate = default,
                CustomerId = 0,
                CustomerName = "",
                BranchId = 0,
                BranchName = "",
                Items = new List<SaleItemDto>() // Invalid: empty list
            };
        }
    }
}
