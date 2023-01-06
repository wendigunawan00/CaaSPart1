using AutoMapper;

namespace CaaS.Api.Profiles;

public class CartDetailsProfile : Profile
{
	public CartDetailsProfile()
	{
		CreateMap<Domain.CartDetails, DTO.CartDetailsDTO>();	
		CreateMap<DTO.CartDetailsDTO, Domain.CartDetails>();	
		
    }
}
