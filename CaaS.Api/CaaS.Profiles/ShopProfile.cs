using AutoMapper;

namespace CaaS.Api.Profiles;

public class ShopProfile : Profile
{
	public ShopProfile()
	{
		CreateMap<Domain.Shop, DTO.ShopDTO>();	
		CreateMap<DTO.ShopDTO, Domain.Shop>();		
    }
}
