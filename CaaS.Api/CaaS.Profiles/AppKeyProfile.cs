using AutoMapper;

namespace CaaS.Api.Profiles;

public class AppKeyProfile : Profile
{
	public AppKeyProfile()
	{
		CreateMap<Domain.AppKey, DTO.AppKeyDTO>();	
		CreateMap<DTO.AppKeyDTO, Domain.AppKey>();		
    }
}
