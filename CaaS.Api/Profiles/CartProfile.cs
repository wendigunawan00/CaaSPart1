using AutoMapper;

namespace CaaS.Api.Profiles;

public class CartProfile : Profile
{
	public CartProfile()
	{
		CreateMap<Domain.Cart, Dtos.CartDTO>();		
		CreateMap<Dtos.CartDTO, Domain.Cart>();		
    }
}
