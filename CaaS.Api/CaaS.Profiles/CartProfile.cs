using AutoMapper;
using AutoMapper;

namespace CaaS.Api.Profiles;

public class CartProfile : Profile
{
	public CartProfile()
	{
		CreateMap<Domain.Cart, DTO.CartDTO>();	
		CreateMap<DTO.CartDTO, Domain.Cart>();		
    }
}
