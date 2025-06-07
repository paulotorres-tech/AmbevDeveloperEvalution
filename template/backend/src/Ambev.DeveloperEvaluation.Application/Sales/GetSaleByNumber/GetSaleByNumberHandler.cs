using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber
{
    public class GetSaleByNumberHandler : IRequestHandler<GetSaleByNumberQuery, GetSaleByNumberResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleByNumberHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSaleByNumberResult> Handle(GetSaleByNumberQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetBySaleNumberAsync(request.SaleNumber, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException($"Sale with number [{request.SaleNumber}] not found.");

            return _mapper.Map<GetSaleByNumberResult>(sale);
        }
    }
}
