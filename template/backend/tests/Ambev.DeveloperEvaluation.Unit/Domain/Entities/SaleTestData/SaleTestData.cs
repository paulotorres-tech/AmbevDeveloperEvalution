using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleTestData
    {
        private static readonly Faker _faker = new();

        public static string GenerateSaleNumber() => _faker.Random.AlphaNumeric(10).ToUpper();

        public static int GenerateCustomerId() => _faker.Random.Int(1, 1000);
        public static string GenerateCustomerName() => _faker.Name.FullName();

        public static int GenerateBranchId() => _faker.Random.Int(1, 50);
        public static string GenerateBranchName() => _faker.Company.CompanyName();

        public static int GenerateProductId() => _faker.Random.Int(1, 1000);

        public static decimal GenerateUnitPrice() => _faker.Random.Decimal(1, 100);


        public static Sale CreateValidSale()
        {
            return new Sale
            (
                saleNumber: GenerateSaleNumber(),
                saleDate: DateTime.UtcNow,
                customerId: GenerateCustomerId(),
                customerName: GenerateCustomerName(),
                branchId: GenerateBranchId(),   
                branchName: GenerateBranchName()
            );
        }
    }
}
