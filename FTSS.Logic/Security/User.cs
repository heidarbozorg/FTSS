using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FTSS.Logic.Security
{
    /// <summary>
    /// This is a repository for users. All users with different roles will be mapped to this class.
    /// </summary>
    public class User
    {
        private readonly User _user;
        public User Model
        {
            get
            {
                return _user;
            }
        }
        public User()
        {
        }

        public User(string token,string key, string issuer)
        {
            this._user = GetData(token,key,issuer);
        }
        /// <summary>
        /// Each JWT will be valid for hours which determines by this constant.
        /// </summary>
        private const int TokenValidationHours = 12;

        #region Properties
        public string Token { get; set; }
		public string WAPIToken { get; set; }
		public int? HokmId { get; set; }
		public string AllHokmIds { get; set; }
		public int? EmailId { get; set; }
        public int? SMSId { get; set; }
		public int? PersonelId { get; set; }


        /// <summary>
        /// The JWT token for this user
        /// </summary>
        public string JWT { get; set; }
        #endregion Properties

        #region Private methods        
        /// <summary>
        /// Get user information as a Claim array for creating JWT
        /// </summary>
        /// <returns></returns>
        private Claim[] GetClaims(string wapiToken)
        {
            if (HokmId == null)
                HokmId = 0;
            if (EmailId == null)
                EmailId = 0;
            if (SMSId == null)
                SMSId = 0;

            var rst = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, this.PersonelId.ToString()),
                new Claim("Token", this.Token),
                new Claim("WAPIToken", wapiToken),
                new Claim("HokmId", this.HokmId.ToString()),
                new Claim("EmailId", this.EmailId.ToString()),
                new Claim("SMSId", this.SMSId.ToString()),
                new Claim("AllHokmIds", this.AllHokmIds)
            };

            return rst;
        }
        #endregion Private methods
        private static string GetJWTToken(IHttpContextAccessor ctx)
        {
            try
            {
                if (ctx.HttpContext == null)
                {
                    throw new Exception("Context Is Null");
                }
                var headers = ctx.HttpContext.Request.Headers["Authorization"];
                if (headers.Count == 0)
                    //در صورتی که نتوانستی اطلاعات را بدست بیاوری، با حروف کوچک امتحان کن
                    headers = ctx.HttpContext.Request.Headers["authorization"];

                if (headers.Count == 0)
                    return null;

                var header = headers.FirstOrDefault();
                string token = "";
                if (header != null)
                {
                    token = header.Replace("Bearer ", "").Replace("bearer ", "");
                }

                return (token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private ClaimsPrincipal GetPrincipal(string token,string key,string issuer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            var symmetricKey = Encoding.UTF8.GetBytes(key);

            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer=issuer,
                ValidAudience=issuer,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return principal;
        }
        public static User GetUserModel(string key, string issuer, IHttpContextAccessor _IHttpContextAccessor)
        {
            try
            {
                var userModel = new User(GetJWTToken(_IHttpContextAccessor),key,issuer);
                if (userModel == null || !userModel.IsValid())
                    return new User();

                return userModel.Model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool IsValid()
        {
            if (_user == null || _user.PersonelId <= 0)
                return false;

            return true;
        }

        private User GetData(string token,string key, string issuer)
        {
            var validateToken = GetPrincipal(token,key, issuer);

            if (validateToken != null)
            {
                var model = new User();
                if (validateToken.Identity != null && !string.IsNullOrEmpty(validateToken.Identity.Name))
                {
                    model.PersonelId = Convert.ToInt32(validateToken.Identity.Name);
                    var EmailId = getValueFromClaim(validateToken.Claims, "EmailId");
                    if (EmailId != null)
                        model.EmailId = int.Parse(EmailId);

                    model.AllHokmIds = getValueFromClaim(validateToken.Claims, "AllHokmIds");
                    var HokmId = getValueFromClaim(validateToken.Claims, "HokmId");
                    if (HokmId != null)
                        model.HokmId = Convert.ToInt32(HokmId);

                    var SMSId = getValueFromClaim(validateToken.Claims, "SMSId");
                    if (SMSId != null)
                        model.SMSId = Convert.ToInt32(SMSId);
                    model.Token = getValueFromClaim(validateToken.Claims, "Token");
                    model.WAPIToken = getValueFromClaim(validateToken.Claims, "WAPIToken");
                    return model;
                }
            }

            return null;
        }
        /// <summary>
        /// Generate JWT based on the User information
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        public Models.Database.DBResult GetJWT(string key, string issuer,string wapiToken, DateTime? expireDate = null)
        {
            //Get user claims
            var claims = GetClaims(wapiToken);

            //Set token expire date-time
            if (expireDate == null)
                expireDate = DateTime.Now.AddHours(TokenValidationHours);

            //Generate and set the JWT
            this.JWT = Security.JWT.Generate(claims, key, issuer, expireDate.Value);

            //Return result as DBResult object
            return new Models.Database.DBResult(200, "", this, 1);
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
