using Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByNumber;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class GetSaleRequestProfile : Profile
    {
        public GetSaleRequestProfile()
        {
            CreateMap<GetSaleByNumberRequest, GetSaleByNumberQuery>();

            CreateMap<GetSaleByNumberResult, GetSaleByNumberResponse>();
            CreateMap<SaleItemResult, GetSaleItemResponse>();
        }
    }
}
