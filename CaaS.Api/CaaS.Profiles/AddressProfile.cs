using AutoMapper;

namespace CaaS.Api.Profiles;

public class AddressProfile : Profile
{
	public AddressProfile()
	{
		CreateMap<Domain.Address, DTO.AddressDTO>();	
		CreateMap<DTO.AddressDTO, Domain.Address>();
		CreateMap<Domain.Address, DTO.AddressForCreationDTO>();	
		CreateMap<DTO.AddressForCreationDTO, Domain.Address>();		
    }
}
