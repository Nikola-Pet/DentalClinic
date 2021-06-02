using Dental.Data;
using Dental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dental.Data
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly DentalClinicContext _context;
        


        public AuthenticateService(DentalClinicContext context)
        {
            _context = context;
        
        }

        public async Task AddPatientAsync(Patient patient)
        {
            _context.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<Patient> GetPatientAsync(string eMail, string password)
        {
           
             var user =  _context.Patients.FirstOrDefault(x => x.Email == eMail && x.Password == Utility.Encrypt(password));
             return user;
        }

        public async Task<Dentist> GetDentistAsync(string eMail, string password)
        {
            var user = _context.Dentists.FirstOrDefault(x => x.Email == eMail && x.Password == password);
            return user;
        }


        public string GenerateToken(string id ,string eMail, string rola)
        {
            string key = "simplekeysimplekeysimplekey";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, eMail),
                    new Claim(ClaimTypes.Role, rola),
                    new Claim(ClaimTypes.NameIdentifier, id)

                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string Token = tokenHandler.WriteToken(token);


            return Token;

        }
        public JwtSecurityToken GetJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("simplekeysimplekeysimplekey");

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,

            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken;
        }

        public string ValidateRoleJwtToken(string token)
        {
            var jwtToken = GetJwtToken(token);

            var r = jwtToken.Claims.First(x => x.Type == "role").ToString();
            string rola = r.Remove(0, 6);
            // return account id from JWT token if validation successful
            return rola;
        }

        public string ValidateEmailJwtToken(string token)
        {
            var jwtToken = GetJwtToken(token);

            var e = jwtToken.Claims.First(x => x.Type == "email").ToString();
            string email = e.Remove(0, 7);
            return email;
        }

        public string ValidateIdJwtToken(string token)
        {
            var jwtToken = GetJwtToken(token);

            var e = jwtToken.Claims.First(x => x.Type == "nameid").ToString();
            string id = e.Remove(0, 8);
            return id;
        }

       













    }
}
