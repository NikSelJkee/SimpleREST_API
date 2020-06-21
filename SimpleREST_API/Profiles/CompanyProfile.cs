using AutoMapper;
using SimpleREST_API.Dtos;
using SimpleREST_API.Entities;

namespace SimpleREST_API.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyForCreatingDto, Company>();
        }
    }
}
