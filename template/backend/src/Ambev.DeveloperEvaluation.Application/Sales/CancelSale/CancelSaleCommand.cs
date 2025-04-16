﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleCommand : IRequest<CancelSaleResult>
    {

        public Guid? SaleId { get; set; }
        public string? SaleNumber { get; set; }
    }
}
