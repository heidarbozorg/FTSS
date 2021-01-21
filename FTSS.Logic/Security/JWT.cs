using FTSS.Models.Database;
using FTSS.Models.Database.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FTSS.Logic.Security
{
    public class JWT : IToken<UserInfo>
    {
        private const string key = "501b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

        /// <summary>
        /// Generate JWT token from UserInfo object
        /// </summary>
        /// <param name="Data">UserInfo object</param>
        /// <returns>JWT token which is a string</returns>
        public string GenerateToken(UserInfo Data)
        {
            var User = Data.User;
            var AccessMenu = Data.AccessMenu;
            var accessMenuJSON = CommonOperations.JSON.ObjToJson(AccessMenu);

            var symmetricKey = Convert.FromBase64String(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, Data.Username),
                    new Claim("FirstName", User.FirstName),
                    new Claim("LastName", User.LastName),
                    new Claim("UserId", User.UserId.ToString()),
                    new Claim("Token", User.Token),
                    new Claim("AccessMenu", accessMenuJSON),
                    new Claim("scope", Guid.NewGuid().ToString()),
                }),
                Expires = User.ExpireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public UserInfo GetData(string Token)
        {
            var validateToken = GetPrincipal(Token);
            
            if (validateToken != null)
            {
                var model = new UserInfo();
                if (validateToken.Identity != null && !string.IsNullOrEmpty(validateToken.Identity.Name))
                {
                    model.Username = validateToken.Identity.Name;
                    var UserId = getValueFromClaim(validateToken.Claims, "UserId");
                    if (UserId != null)
                        model.User.UserId = int.Parse(UserId);

                    model.User.FirstName = getValueFromClaim(validateToken.Claims, "FirstName");
                    model.User.LastName = getValueFromClaim(validateToken.Claims, "LastName");
                    model.User.Token = getValueFromClaim(validateToken.Claims, "token");
                    var accessMenuJSON = getValueFromClaim(validateToken.Claims, "AccessMenu");
                    if (!string.IsNullOrEmpty(accessMenuJSON))
                        model.AccessMenu = CommonOperations.JSON.jsonToT<Models.Database.StoredProcedures.SP_User_GetAllMenu>(accessMenuJSON);

                    return model;
                }
            }

            return null;
        }


        private ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var symmetricKey = Convert.FromBase64String(key);

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return principal;
        }

        /// <summary>
        /// Get a value from Claim by it's name
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string getValueFromClaim(IEnumerable<Claim> claims, string Name)
        {
            try
            {
                var item = claims.FirstOrDefault(a => a.Type.ToLower().Equals(Name.ToLower()));
                return (item.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
