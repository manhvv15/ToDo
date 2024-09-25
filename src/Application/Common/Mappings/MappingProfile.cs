using ToDo.Application.Features.ProductFeatures.Queries;
using ToDo.Application.OrderDetailFeatures.Queries;
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
