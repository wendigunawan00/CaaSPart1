using CaaS.DTO;
using AutoMapper;
using CaaS.Domain;

namespace CaaS.Features
{
    public interface IAuth
    {
       Task<string?> Authenticate(AdminDTO user);
       public void setMapper(IMapper mapper);
    }
}
