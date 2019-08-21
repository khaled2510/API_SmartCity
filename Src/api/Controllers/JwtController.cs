using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly Infrastructure.JwtIssuerOptions _jwtOptions;
        private dal.Hash hash;
        public JwtController(IOptions<Infrastructure.JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            this.hash = new dal.Hash();
        }

        // POST api/Jwt
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] model.LoginModel loginModel)
        {
            if(loginModel.UserName != "pseudo3" && loginModel.UserName != "pseudo1" && loginModel.UserName != "pseudo2") // pour me permettre de me connecter avec les comptes créé via les insert dans la DB pour mes tests
            {
                string hashPassword = hash.ComputeSha256Hash(loginModel.Password);
                loginModel.Password = hashPassword;
            }

            var repository = new Infrastructure.AuthenticationRepositiry();
            
            model.Utilisateur userFound = repository.GetUsers().FirstOrDefault(user => user.Pseudo == loginModel.UserName 
            && user.MotDePasse == loginModel.Password);

            if(userFound == null)
            {
                return Unauthorized();
            }
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userFound.Pseudo),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                         ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                         ClaimValueTypes.Integer64),
            };

            if (userFound.Role != null)
            {
                claims.Add(new Claim("roles", userFound.Role));
            }

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            var response = new 
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            return Ok(response);
        }
        private static long ToUnixEpochDate(DateTime date)
                => (long)Math.Round((date.ToUniversalTime() -
                                    new DateTimeOffset(1970, 1, 1,
                                    0, 0, 0, TimeSpan.Zero)).TotalSeconds);    
    }
}
