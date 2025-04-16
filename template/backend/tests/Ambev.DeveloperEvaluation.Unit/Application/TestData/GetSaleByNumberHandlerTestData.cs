using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class GetSaleByNumberHandlerTestData
    {
        private static readonly Faker _faker = new();

        public static GetSaleByNumberQuery ValidQuery()
        {
            return new GetSaleByNumberQuery
            {
                SaleNumber = "SALE001"
            };
        }

        public static GetSaleByNumberQuery InvalidQuery()
        {
            return new GetSaleByNumberQuery
            {
                SaleNumber = "NOTFOUND123"
            };
        }

        public static Sale GenerateValidSale()
        {
            var sale = new Sale("SALE001", DateTime.UtcNow, 1, "John Doe", 1, "Main Branch");

            sale.AddItem(productId: 101, quantity: 3, unitPrice: 50);
            sale.AddItem(productId: 102, quantity: 5, unitPrice: 20);

            return sale;
        }
    }
}
