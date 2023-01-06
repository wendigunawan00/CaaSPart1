using CaaS.DTO;
using AutoMapper;


namespace CaaS.Features
{
    public interface IAuth
    {
        public Task<string?> Authenticate(AdminDTO user);

    }
}
