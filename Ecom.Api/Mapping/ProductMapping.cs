using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;

namespace Ecom.Api.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product,ProductDto>()
                .ForMember(x => x.CategoryName , option => option.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<Photo,PhotoDto>().ReverseMap();
            CreateMap<AddProductDto,Product>()
                .ForMember(m=> m.Photos,op=> op.Ignore()).ReverseMap();
            CreateMap<UpdateProductDto,Product>()
                .ForMember(m=> m.Photos,op=> op.Ignore()).ReverseMap();

        }
    }
}
