using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common;
using Microsoft.IdentityModel.Tokens;

namespace Web.Admin.App
{
    public static class JwtHelpers
    {
        public static bool ValidateToken(string token, out UserData userData)
        {
            userData = null;

            if (!ValidateToken(token, new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey("secret-key-asd-qwe".GetBytes())
            }, out var claims))
                return false;
            
            var tokenExpiresAtUtc = DateTime.Parse(claims.FindFirstValue("expiresAtUtc"), styles: DateTimeStyles.AdjustToUniversal);
            if (tokenExpiresAtUtc < DateTime.UtcNow)
                return false;

            userData = claims.FindFirstValue("data").FromJson<UserData>();

            return true;
        }

        public static string GenerateToken(IEnumerable<Claim> claims, DateTime? expires = null)
        {
            if (claims == null)
                throw new ArgumentNullException(nameof(claims));
            
            var credentials = new SigningCredentials(new SymmetricSecurityKey("secret-key-asd-qwe".GetBytes()), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: credentials, claims: claims, notBefore: DateTime.UtcNow);

            try
            {
                new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        private static bool ValidateToken(string token, TokenValidationParameters validationParameters, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }

        public class UserData
        {
            public int Id { get; set; }
            public string Email { get; set; }
        }
    }
}