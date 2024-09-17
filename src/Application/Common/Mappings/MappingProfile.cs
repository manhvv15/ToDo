using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Features.OrderDetailFeatures.Queries;
using ToDo.Application.Features.ProductFeatures.Queries;
using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<OrderDetail, OrderDetailDto>();
    }
}
