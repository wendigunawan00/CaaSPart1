using AutoMapper;

namespace CaaS.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CaaS.Domain.Product, CaaS.DTO.ProductDTO>();
        CreateMap<DTO.ProductForCreationDTO, Domain.Product>();
    }
}
