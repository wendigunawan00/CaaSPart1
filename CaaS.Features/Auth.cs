using AutoMapper;
using CaaS.Domain;
using CaaS.DTO;
using CaaS.Logic;
using Dal.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CaaS.Features
{
    public class Auth : IAuth
    {
        private readonly IManagementLogic<Shop> logicShop;
        private readonly IManagementLogic<Person> logicPerson;
        private readonly IManagementLogic<AppKey> logicAppKey;
       
        

        public Auth(IManagementLogic<Shop> logicShop, IManagementLogic<Person> logicPerson,
            IManagementLogic<AppKey> logicAppKey
            )
        {
            this.logicShop = logicShop ?? throw new ArgumentNullException(nameof(logicShop));
            this.logicPerson = logicPerson ?? throw new ArgumentNullException(nameof(logicPerson));
            this.logicAppKey = logicAppKey ?? throw new ArgumentNullException(nameof(logicAppKey));           
            
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
            var mandant = (await logicPerson.Get()).Where(m=>m.Email.Equals(user.Email));
            var appKey = (await logicAppKey.Get()).Where(ak => ak.AppKeyName.Equals(user.AppKeyName));
            if(mandant.IsNullOrEmpty() || appKey.IsNullOrEmpty())
            {
                return null;
            }
            var shops = (await logicShop.Get()).Where(s=>s.MandantId.Equals(mandant.First().Id) && s.AppKey.Equals(appKey.First().Id));
            if (shops.IsNullOrEmpty())
            {
                return null;
            }
            return GetToken(user);
           
        }
        
       
        
    }
}
