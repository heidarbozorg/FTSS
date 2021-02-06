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
    public class UserJWT : Models.Database.StoredProcedures.SP_Login.Outputs, IToken
    {
        public string JWT { get; set; }

        /// <summary>
        /// Set user fields
        /// </summary>
        /// <param name="data"></param>
        private void Initial(Models.Database.StoredProcedures.SP_Login.Outputs data)
        {
            this.Email = data.Email;
            this.FirstName = data.FirstName;
            this.LastName = data.LastName;
            this.Token = data.Token;
            this.ExpireDate = data.ExpireDate;
            this.RoleTitle = data.RoleTitle;
        }

        /// <summary>
        /// Initialize fields and generate JWT
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        public UserJWT(Models.Database.DBResult data, string key, string issuer)
        {
            if (data == null || data.Data == null)
                throw new ArgumentNullException("In UserJWT, user could not be null.");

            if (!(data.Data is Models.Database.StoredProcedures.SP_Login.Outputs))
                throw new ArgumentException("Data is not valid.");

            Initial(data.Data as Models.Database.StoredProcedures.SP_Login.Outputs);
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
    }
}