using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    /// <summary>
    /// Request to cancel a sale by ID or SaleNumber.
    /// </summary>
    public class CancelSaleRequest : IRequest<CancelSaleResponse>
    {
        public Guid? SaleId { get; set; }
        public string? SaleNumber { get; set; }
    }
}
