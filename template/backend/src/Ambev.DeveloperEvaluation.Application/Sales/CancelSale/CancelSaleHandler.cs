using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public CancelSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            if (command.SaleId.HasValue)
            {
                var success = await _saleRepository.CancelByIdAsync(command.SaleId.Value, cancellationToken);
                return new CancelSaleResult
                {
                    Success = success,
                    Message = success ? "Sale successfully cancelled." : "Sale not found or already cancelled."
                };
            }

            if (!string.IsNullOrWhiteSpace(command.SaleNumber))
            {
                var success = await _saleRepository.CancelBySaleNumberAsync(command.SaleNumber, cancellationToken);
                return new CancelSaleResult
                {
                    Success = success,
                    Message = success ? "Sale successfully cancelled." : "Sale not found or already cancelled."
                };
            }

            return new CancelSaleResult
            {
                Success = false,
                Message = "Either SaleId or SaleNumber must be provided."
            };
        }
    }
}
