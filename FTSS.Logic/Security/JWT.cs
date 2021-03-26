using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FTSS.Logic.Security
{
    public class JWT
    {
        /// <summary>
        /// Generate token validation
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <returns></returns>
        public static TokenValidationParameters GetTokenValidationParameters(string key, string issuer)
        {
            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer))
                throw new ArgumentNullException("key and issuer could not be empty. Check application settings.");

            var rst = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            return rst;
        }

        /// <summary>
        /// Get authentication event handler by JWT
        /// </summary>
        /// <returns></returns>
        public static JwtBearerEvents GetJWTEvents()
        {
            var rst = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        context.Response.Headers.Add("Token-Expired", "true");

                    return Task.CompletedTask;
                }
            };

            return rst;
        }


        /// <summary>
        /// Validating input params
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <param name="expireDate"></param>
        private static void Validation(Claim[] claims, string key, string issuer, DateTime expireDate)
        {
            if (claims == null || claims.Count() == 0)
                throw new ArgumentNullException("In GenerateJWT, claims could not be null.");

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("In GenerateJWT, key could not be empty.");

            if (string.IsNullOrWhiteSpace(issuer))
                throw new ArgumentNullException("In GenerateJWT, issuer could not be empty.");

            if (expireDate < DateTime.Now)
                throw new ArgumentException("In GenerateJWT, expireDate could not be before the current date.");
        }

        /// <summary>
        /// Generate JWT based on the claims and security parameters
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <param name="expireDate"></param>
        /// <returns></returns>
        public static string Generate(Claim[] claims, string key, string issuer, DateTime expireDate)
        {
            Validation(claims, key, issuer, expireDate);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer,     //Issure  
                issuer,     //Audience
                claims,
                expires: expireDate,
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
