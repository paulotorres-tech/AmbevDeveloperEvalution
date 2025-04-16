using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Serilog.Parsing;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleByNumber
{
    public class GetSaleByNumberProfile : Profile
    {
        public GetSaleByNumberProfile() {
            CreateMap<Sale, GetSaleByNumberResult>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));

            CreateMap<SaleItem, SaleItemResult>()
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(i =>
                    (i.UnitPrice * i.Quantity) - ((i.UnitPrice * i.Quantity) * i.DiscountPercent)));
        }
    }
}
