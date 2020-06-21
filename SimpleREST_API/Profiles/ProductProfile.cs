using AutoMapper;
using SimpleREST_API.Dtos;
using SimpleREST_API.Entities;

namespace SimpleREST_API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
