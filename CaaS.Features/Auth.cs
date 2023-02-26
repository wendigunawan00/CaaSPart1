using AutoMapper;
using CaaS.Domain;
using CaaS.DTO;
using static CaaS.Dal.Ado.AdoMapDao;
using CaaS.Logic;
using Dal.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CaaS.Dal.Ado;

namespace CaaS.Features
{
    public class Auth : IAuth
    {
        private IMapper _mapper;
        private readonly AdoPersonDao logicPerson;
        private readonly AdoAppKeyDao logicAppKey;
        private readonly string[] TenantAppKey;

        public void setMapper(IMapper value)
        {
            _mapper = value;
        }

        public Auth( AdoPersonDao logicPerson, AdoAppKeyDao logicAppKey, string[] TenantAppKey)
        {           
            this.logicPerson = logicPerson;
            this.logicAppKey = logicAppKey;
            this.TenantAppKey = TenantAppKey;
        }

        private async Task<PersonDTO?> GetAdminByEmail(string email)
        {           
            return _mapper.Map<PersonDTO>(await logicPerson.FindTByX(email, TenantAppKey[0]));
        }

        private async Task<AppKeyDTO?> GetAppKey(string appKeyName)
        {
            return _mapper.Map<AppKeyDTO>(await logicAppKey.FindTByX(appKeyName, TenantAppKey[1]));
        }                

        private string GetToken(AdminDTO admin)
        {
            IConfiguration config = ConfigurationUtil.GetConfiguration();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email , admin.Email),
                new Claim(ClaimTypes.Sid , admin.AppKeyName)
            };

            var token = new JwtSecurityToken(
                config["Jwt:ValidIssuer"],
                config["Jwt:ValidAudience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }
        public async Task<string?> Authenticate(AdminDTO user)
        {
            var mandant = await GetAdminByEmail(user.Email);                
            var appKey = await GetAppKey(user.AppKeyName);
            if(mandant is null || appKey is null)
            {
                return null;
            }
            if (mandant.Status.Equals(appKey.ShopId))
            {
                return GetToken(user);           
            }

            return null;
        }
      
    }
}
