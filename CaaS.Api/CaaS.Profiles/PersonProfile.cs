using AutoMapper;

namespace CaaS.Api.Profiles;

public class PersonProfile : Profile
{
	public PersonProfile()
	{
		CreateMap<Domain.Person, DTO.PersonDTO>();	
		CreateMap<Domain.Person, DTO.PersonForCreationDTO>();	
		CreateMap<DTO.PersonDTO, Domain.Person>();		
		CreateMap<DTO.PersonForCreationDTO, Domain.Person>();		
    }
}
