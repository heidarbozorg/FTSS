using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FTSS.Logic.Security
{
    public class UserJWT : Models.Database.StoredProcedures.SP_Login.Outputs, IToken
    {
        #region Properties
        /// <summary>
        /// The user database token
        /// </summary>
        /// <remarks>
        /// This field should not be reported to the end-point.
        /// </remarks>
        private string Token { get; set; }

        /// <summary>
        /// Since I want to hide this field for end-point, I re-define it as a private property.
        /// </summary>
        private int UserId { get; set; }

        /// <summary>
        /// The JWT token for this user
        /// </summary>
        public string JWT { get; set; }
        #endregion

        /// <summary>
        /// Initialize fields and generate JWT
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        private void Generate(string key, string issuer)
        {
            var t = Token;
            this.JWT = Common.GenerateJWT(GetClaims(), key, issuer, this.ExpireDate);
        }

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
                new Claim("FirstName", this.FirstName ?? ""),
                new Claim("LastName", this.LastName ?? ""),
                new Claim("Token", this.Token),
                new Claim("role", this.RoleTitle)
            };

            return rst;
        }
    
        /// <summary>
        /// Generate JWT based on the Login results
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static UserJWT Get(Models.Database.DBResult data, string key, string issuer, AutoMapper.IMapper mapper)
        {
            if (data == null || data.Data == null)
                throw new ArgumentNullException("In UserJWT, user could not be null.");

            if (!(data.Data is Models.Database.StoredProcedures.SP_Login.Outputs))
                throw new ArgumentException("Data is not valid.");

            var loginResult = data.Data as Models.Database.StoredProcedures.SP_Login.Outputs;
            var userJWT = mapper.Map<UserJWT>(loginResult);
            userJWT.Token = loginResult.Token;

            userJWT.Generate(key, issuer);
            return userJWT;
        }
    }
}