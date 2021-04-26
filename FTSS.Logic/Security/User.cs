using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FTSS.Logic.Security
{
    /// <summary>
    /// This is a repository for users. All users with different roles will be mapped to this class.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Each JWT will be valid for hours which determines by this constant.
        /// </summary>
        private const int TokenValidationHours = 12;

        #region Properties
        public string Token { get; set; }

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
        private Claim[] GetClaims()
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
                new Claim("HokmId", this.HokmId.ToString()),
                new Claim("EmailId", this.EmailId.ToString()),
                new Claim("SMSId", this.SMSId.ToString()),
                new Claim("AllHokmIds", this.AllHokmIds)
            };

            return rst;
        }
        #endregion Private methods

        /// <summary>
        /// Generate JWT based on the User information
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        public Models.Database.DBResult GetJWT(string key, string issuer, DateTime? expireDate = null)
        {
            //Get user claims
            var claims = GetClaims();

            //Set token expire date-time
            if (expireDate == null)
                expireDate = DateTime.Now.AddHours(TokenValidationHours);

            //Generate and set the JWT
            this.JWT = Security.JWT.Generate(claims, key, issuer, expireDate.Value);

            //Return result as DBResult object
            return new Models.Database.DBResult(200, "", this, 1);
        }
    }
}
