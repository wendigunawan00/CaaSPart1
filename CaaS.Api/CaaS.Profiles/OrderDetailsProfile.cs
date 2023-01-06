using AutoMapper;

namespace CaaS.Api.Profiles;

public class OrderDetailsProfile : Profile
{
	public OrderDetailsProfile()
	{
		CreateMap<Domain.OrderDetails, DTO.OrderDetailsDTO>();	
		CreateMap<DTO.OrderDetailsDTO, Domain.OrderDetails>();	
		
    }
}
