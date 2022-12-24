using AutoMapper;

namespace CaaS.Api.Profiles;

public class OrderProfile : Profile
{
	public OrderProfile()
	{
		CreateMap<Domain.Order, Dtos.OrderDTO>();
	}
}
