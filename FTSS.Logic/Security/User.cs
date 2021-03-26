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
        public string DbToken { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public Roles Role { get; set; }

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
            var rst = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, this.Email),
                new Claim("DbToken", this.DbToken),
                new Claim("Firstname", this.Firstname ?? ""),
                new Claim("Lastname", this.Lastname ?? ""),
                new Claim("role", this.Role.ToString())
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
