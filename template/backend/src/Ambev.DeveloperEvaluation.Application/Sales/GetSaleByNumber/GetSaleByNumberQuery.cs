using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber
{
    public class GetSaleByNumberQuery : IRequest<GetSaleByNumberResult>
    {
        public string SaleNumber { get; set; } = string.Empty;
    }
}
