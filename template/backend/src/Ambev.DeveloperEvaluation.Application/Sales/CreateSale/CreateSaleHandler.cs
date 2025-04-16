using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _salesRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(ISaleRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validatorResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validatorResult.IsValid)
                throw new ValidationException(validatorResult.Errors);

            var existing = await _salesRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
            if (existing != null)
                throw new InvalidOperationException($"A sale with number {command.SaleNumber} already exists in repository.");

            var sale = new Sale(
                saleNumber: command.SaleNumber,
                saleDate: command.SaleDate,
                customerId: command.CustomerId,
                customerName: command.CustomerName,
                branchId: command.BranchId,
                branchName: command.BranchName);

            foreach (var item in command.Items)
            {
                sale.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            }

            var created = await _salesRepository.CreateAsync(sale, cancellationToken);

            return new CreateSaleResult { Id = created.Id };
        }
    }
}
