using FTSS.Models.Database;
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
        public const string key = "501b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
        private string _key
        {
            get;
            set;
        }

        private readonly UserInfo _user;
        public UserInfo User
        {
            get
            {
                return _user;
            }
        }

        public ClaimsPrincipal Claims
        {
            get; set;
        }


        private UserInfo GetData(string token)
        {
            Claims = GetPrincipal(token);

            if (Claims != null)
            {
                var model = new UserInfo();
                if (Claims.Identity != null && !string.IsNullOrEmpty(Claims.Identity.Name))
                {
                    model.Username = Claims.Identity.Name;
                    var UserId = getValueFromClaim(Claims.Claims, "UserId");
                    if (UserId != null)
                        model.User.UserId = int.Parse(UserId);

                    model.User.FirstName = getValueFromClaim(Claims.Claims, "FirstName");
                    model.User.LastName = getValueFromClaim(Claims.Claims, "LastName");
                    model.User.Token = getValueFromClaim(Claims.Claims, "token");
                    var accessMenuJSON = getValueFromClaim(Claims.Claims, "AccessMenu");
                    if (!string.IsNullOrEmpty(accessMenuJSON) && accessMenuJSON != "null")
                        model.AccessMenu = CommonOperations.JSON.jsonToT<List<Models.Database.StoredProcedures.SP_User_GetAccessMenu>>(accessMenuJSON);

                    return model;
                }
            }

            return null;
        }

        /// <summary>
        /// Generate JWT token from UserInfo object
        /// </summary>
        /// <param name="data">UserInfo object</param>
        /// <returns>JWT token which is a string</returns>
        public string GenerateToken(UserInfo data)
        {
            var user = data.User;
            var accessMenu = data.AccessMenu;
            var accessMenuJSON = CommonOperations.JSON.ObjToJson(accessMenu);

            //var symmetricKey = Convert.FromBase64String(_key);
            var symmetricKey = Convert.FromBase64String(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, data.Username),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Token", user.Token),
                    new Claim("AccessMenu", accessMenuJSON),
                    new Claim("scope", Guid.NewGuid().ToString()),
                }),
                Expires = user.ExpireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }


        public bool IsValid()
        {
            if (_user == null || _user.User == null || _user.User.UserId <= 0)
                return false;
            
            return true;
        }

        public JWT()
        {
        }

        public JWT(string token)
        {
            this._user = GetData(token);
        }

        public JWT(string t, string key)
        {
            this._key = key;
        }

        private ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            //var symmetricKey = Convert.FromBase64String(_key);
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
        /// <param name="name"></param>
        /// <returns></returns>
        private string getValueFromClaim(IEnumerable<Claim> claims, string name)
        {
            try
            {
                var item = claims.FirstOrDefault(a => a.Type.ToLower().Equals(name.ToLower()));
                return (item.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
