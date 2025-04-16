using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CancelSaleHandlerTestData
    {
        public static CancelSaleCommand ValidByIdCommand()
        {
            return new CancelSaleCommand
            {
                SaleId = Guid.NewGuid()
            };
        }

        public static CancelSaleCommand ValidBySaleNumberCommand()
        {
            return new CancelSaleCommand
            {
                SaleNumber = new Faker().Random.AlphaNumeric(10).ToUpper()
            };
        }

        public static CancelSaleCommand InvalidCommand() => new(); // Both null

        public static CancelSaleCommand InvalidByIdCommand(Guid id)
        {
            return new CancelSaleCommand
            {
                SaleId = id
            };
        }

        public static CancelSaleCommand InvalidBySaleNumberCommand(string number)
        {
            return new CancelSaleCommand
            {
                SaleNumber = number
            };
        }
    }
}
