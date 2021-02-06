using FTSS.Models.Database;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FTSS.Logic.Security
{
    public class UserInfo
    {
        public readonly Logic.Database.IDatabaseContext _ctx;

        public UserInfo(Logic.Database.IDatabaseContext ctx)
        {
            this._ctx = ctx;
        }

        public UserInfo()
        {
            this.User = new Models.Database.StoredProcedures.SP_Login();
            this.AccessMenu = new List<Models.Database.StoredProcedures.SP_User_GetAccessMenu>();
        }

        public string Username { get; set; }

        /// <summary>
        /// User information
        /// </summary>
        public Models.Database.StoredProcedures.SP_Login User { get; set; }

        /// <summary>
        /// Application menu and restful APIs
        /// </summary>
        public List<Models.Database.StoredProcedures.SP_User_GetAccessMenu> AccessMenu { get; set; }

        /// <summary>
        /// Call database stored procedure for username & password validation
        /// </summary>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        private DBResult Login(Models.Database.StoredProcedures.SP_Login_Params filterParams)
        {
            var LoginResult = Logic.Database.StoredProcedure.SP_Login.Call(_ctx, filterParams);
            if (LoginResult.ErrorCode != 200)
                return LoginResult;

            //Set user info
            this.User = LoginResult.Data as Models.Database.StoredProcedures.SP_Login;
            this.Username = filterParams.Email;

            //Get user access menu
            var AccessMenuResult = Logic.Database.StoredProcedure.SP_User_GetAccessMenu.Call(_ctx, this.User);
            if (AccessMenuResult.ErrorCode != 200)
                return AccessMenuResult;

            //Set user access menu
            this.AccessMenu = AccessMenuResult.Data as List<Models.Database.StoredProcedures.SP_User_GetAccessMenu>;

            //Create result
            var rst = new DBResult(200, "", this);
            return rst;
        }

        /// <summary>
        /// Login and generate JWT token
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="filterParams"></param>
        /// <returns></returns>
        public static DBResult Login(Logic.Database.IDatabaseContext ctx, Models.Database.StoredProcedures.SP_Login_Params filterParams)
        {
            //Validation Username & Password by database stored procedure
            var userInfo = new UserInfo(ctx);
            var loginResult = userInfo.Login(filterParams);

            //If login failed, exit
            if (loginResult.ErrorCode != 200)
                return loginResult;

            //Generate JWT token
            var jwt = new JWT();
            var jwtToken = jwt.GenerateToken(loginResult.Data as UserInfo);
            
            //Create result
            var rst = new DBResult(200, "", jwtToken);
            return rst;
        }

        public static DBResult Login2(Logic.Database.IDatabaseContext ctx, Models.Database.StoredProcedures.SP_Login_Params filterParams)
        {
            var LoginResult = Logic.Database.StoredProcedure.SP_Login.Call(ctx, filterParams);
            if (LoginResult.ErrorCode != 200)
                return LoginResult;

            //Set user info
            var user = LoginResult.Data as Models.Database.StoredProcedures.SP_Login;

            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Logic.Security.JWT.key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, filterParams.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Token", user.Token),
            };

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken
            (
                issuer,     //Issure    
                issuer,     //Audience    
                permClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            var rst = new DBResult(200, "", jwt_token);
            return rst;
        }
    }
}
