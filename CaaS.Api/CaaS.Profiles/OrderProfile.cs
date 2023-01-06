using AutoMapper;

namespace CaaS.Profiles;

public class OrderProfile : Profile
{
	public OrderProfile()
	{
		CreateMap<Domain.Order, DTO.OrderDTO>();
		CreateMap<DTO.OrderDTO, Domain.Order>();
	}
}
