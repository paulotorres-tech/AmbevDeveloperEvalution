using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public  class SaleItemValidator : AbstractValidator<SaleItemDto>
    {
        public SaleItemValidator() {
            RuleFor(item => item.ProductId)
                .GreaterThan(0);

            RuleFor(item => item.Quantity)
                .GreaterThan(0)
                .LessThanOrEqualTo(20);

            RuleFor(item => item.UnitPrice)
                .GreaterThan(0);
        }
    }
}
