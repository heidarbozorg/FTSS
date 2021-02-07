using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FTSS.Logic.Security
{
    public class Common
    {
        /// <summary>
        /// Generate JWT
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="expireDate"></param>
        /// <param name="key"></param>
        /// <param name="issuer"></param>
        /// <returns></returns>
        public static string GenerateJWT(Claim[] claims, string key, string issuer, DateTime expireDate)
        {
            if (claims == null || claims.Count() == 0)
                throw new ArgumentNullException("In GenerateJWT, claims could not be null.");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("In GenerateJWT, key could not be empty.");

            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentNullException("In GenerateJWT, issuer could not be empty.");

            if (expireDate < DateTime.Now)
                throw new ArgumentException("In GenerateJWT, expireDate could not be before the current date.");

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

        /// <summary>
        /// Check user authorization by calling a database stored-procedure
        /// </summary>
        /// <param name="dbCTX">Default ORM</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsUserAccessToAPI(Database.IDatabaseContext dbCTX,
            Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs data)
        {
            if (dbCTX == null)
                throw new ArgumentNullException("In IsUserAccessToAPI the dbCTX could not be null.");

            if (data == null)
                throw new ArgumentNullException("In IsUserAccessToAPI the data could not be null.");

            //Calling sp
            var rst = dbCTX.SP_User_AccessToAPI(data);

            //Check result
            if (rst == null || rst.ErrorCode != 200 || rst.Data == null)
                return false;

            if (!(rst.Data is Models.Database.StoredProcedures.SP_User_AccessToAPI.Outputs))
                return false;

            var result = rst.Data as Models.Database.StoredProcedures.SP_User_AccessToAPI.Outputs;
            return result.Result;
        }
    }
}
