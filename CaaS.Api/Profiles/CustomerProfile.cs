using AutoMapper;

namespace CaaS.Api.Profiles;

public class CustomerProfile : Profile
{
	public CustomerProfile()
	{
		CreateMap<Domain.Person, Dtos.PersonDTO>();
		//CreateMap<Dtos.CustomerForCreationDto, Domain.Customer>();
        //CreateMap<Dtos.CustomerForUpdateDto, Domain.Customer>();
    }
}
