using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByNumber
{
    /// <summary>
    /// Request to get sale details by sale number.
    /// </summary>
    public class GetSaleByNumberRequest : IRequest<GetSaleByNumberResponse>
    {
        public string SaleNumber { get; set; } = string.Empty;
    }
}
