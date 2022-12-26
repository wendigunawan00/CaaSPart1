using AutoMapper;

namespace CaaS.Api.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Domain.Product, Dtos.ProductDTO>();
        CreateMap<Dtos.ProductForCreationDTO, Domain.Product>();
    }
}
