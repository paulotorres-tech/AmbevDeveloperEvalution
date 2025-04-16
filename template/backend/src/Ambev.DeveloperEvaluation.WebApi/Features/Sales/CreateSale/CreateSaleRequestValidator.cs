using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Validation rules for CreateSaleRequest.
    /// </summary>
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(r => r.SaleNumber).NotEmpty().Length(3, 50);
            RuleFor(r => r.SaleDate).NotEmpty();
            RuleFor(r => r.CustomerId).GreaterThan(0);
            RuleFor(r => r.CustomerName).NotEmpty().MaximumLength(100);
            RuleFor(r => r.BranchId).GreaterThan(0);
            RuleFor(r => r.BranchName).NotEmpty().MaximumLength(100);
            RuleFor(r => r.Items).NotNull().NotEmpty();

            RuleForEach(r => r.Items).SetValidator(new CreateSaleItemValidator());
        }
    }

    public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemValidator()
        {
            RuleFor(i => i.ProductId).GreaterThan(0);
            RuleFor(i => i.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            RuleFor(i => i.UnitPrice).GreaterThan(0);
        }
    }
}
