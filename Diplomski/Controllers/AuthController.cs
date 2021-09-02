using Diplomski.Data.Interfaces;
using Diplomski.DTOs;
using Diplomski.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAdminRepository _repository;
        private readonly JWTOptions _jwtOptions;

        public AuthController(IAdminRepository repository, IOptions<JWTOptions> jwtOptions)
        {
            _repository = repository;
            _jwtOptions = jwtOptions.Value;
        }
        

        [HttpPost("login")]
        public IActionResult Login(LoginCredentialsDTO loginCredentials)
        {
            if(loginCredentials is null)
            {
                return BadRequest("Neispravan zahtev");
            }

            var admin = _repository.GetAdminByUsernameAndPassword(loginCredentials.Username, loginCredentials.Password);

            if(admin is null)
            {
                return BadRequest("Neispravni kredencijali");
            }

            var token = GenerateToken();

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var tokenExpiration = token.ValidTo;

            var response = new AuthResponseDTO() { Token = tokenString, ExpirationTime = tokenExpiration };

            return Ok(response);
        }


        private JwtSecurityToken GenerateToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: new List<Claim>(),
                expires: DateTime.UtcNow.AddHours(24), // token is valid for 24 hours
                signingCredentials: signingCredentials
            );

            return token;
        }

    }
}
