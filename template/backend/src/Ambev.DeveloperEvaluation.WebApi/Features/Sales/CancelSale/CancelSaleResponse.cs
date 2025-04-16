﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    /// <summary>
    /// Response indicating the result of sale cancellation.
    /// </summary>
    public class CancelSaleResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
