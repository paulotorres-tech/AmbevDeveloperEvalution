namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByNumber
{
    /// <summary>
    /// Response containing full sale data.
    /// </summary>
    public class GetSaleByNumberResponse
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public bool IsCancelled { get; set; }
        public decimal TotalAmount { get; set; }

        public List<GetSaleItemResponse> Items { get; set; } = new();
    }

    public class GetSaleItemResponse
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
