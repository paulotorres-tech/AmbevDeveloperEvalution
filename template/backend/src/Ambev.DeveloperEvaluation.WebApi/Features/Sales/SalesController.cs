using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByNumber;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Controller responsible for managing sales.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("sales")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
        {
            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<CreateSaleResponse>(result);

            return Created(nameof(Create), new { id = result.Id }, response);
        }

        /// <summary>
        /// Retrieves a sale by its sale number.
        /// </summary>
        [HttpGet("{saleNumber}")]
        public async Task<IActionResult> GetByNumber(string saleNumber)
        {
            var request = new GetSaleByNumberRequest { SaleNumber = saleNumber };

            var query = _mapper.Map<GetSaleByNumberQuery>(request);
            var result = await _mediator.Send(query);
            var response = _mapper.Map<GetSaleByNumberResponse>(result);

            return Ok(new ApiResponseWithData<GetSaleByNumberResponse>
            {
                Success = true,
                Data = response
            });
        }


        /// <summary>
        /// Cancels a sale by its sale number.
        /// </summary>
        [HttpPut("{saleNumber}/cancel")]
        public async Task<IActionResult> Cancel(string saleNumber)
        {
            var request = new CancelSaleRequest { SaleNumber = saleNumber };

            var command = _mapper.Map<CancelSaleCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<CancelSaleResponse>(result);

            if (!response.Success)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = response.Message
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = response.Message
            });
        }

    }
}