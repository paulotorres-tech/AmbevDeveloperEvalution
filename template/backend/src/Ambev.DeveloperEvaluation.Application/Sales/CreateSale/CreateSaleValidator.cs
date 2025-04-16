using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator() {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(sale => sale.CustomerId)
                .GreaterThan(0);

            RuleFor(sale => sale.CustomerName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(sale => sale.BranchId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(sale => sale.BranchName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(sale => sale.SaleDate)
                .NotEmpty();

            RuleFor(sale => sale.Items)
                .NotNull()
                .NotEmpty()
                .WithMessage("Sale must contain at least one item.");

            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemValidator());
        }
    }
}
