using application.Interfaces;
using domain.Entities;
using domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace application.Services
{
    public class TokenService:ITokenService
    {

        private readonly IConfiguration _config;
        private readonly IAuthenticateRepository<ObjIdentity> _authenticateRepository;

        public TokenService(IConfiguration config, 
            IAuthenticateRepository<ObjIdentity> authenticateRepository) 
        { 
            _config = config;
            _authenticateRepository = authenticateRepository;
        }   
        
        public async Task<string> GetToken(ObjIdentity identity)
        {
            try
            {
                var result = await _authenticateRepository.GetToken(identity);
                if (result.IsAuthenticated)
                {
                    var claims = new[]
{
                new Claim(ClaimTypes.Name, identity.Name),
               };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("jwt:key").Value));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                    var securityToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

                    string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                    return await Task.FromResult(token);
                }
                else 
                {
                    return "unauthorized";
                }
                

            }
            catch (Exception e) 
            {
                return "ERROR";
            }
        }
    }
}
