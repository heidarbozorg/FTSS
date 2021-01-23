using FTSS.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Security
{
    public class UserInfo
    {
        public readonly Logic.Database.IDBCTX _ctx;

        public UserInfo(Logic.Database.IDBCTX ctx)
        {
            this._ctx = ctx;
        }

        public UserInfo()
        {
            this.User = new Models.Database.StoredProcedures.SP_Login();
            this.AccessMenu = new Models.Database.StoredProcedures.SP_User_GetAccessMenu();
        }

        public string Username { get; set; }

        /// <summary>
        /// User information
        /// </summary>
        public Models.Database.StoredProcedures.SP_Login User { get; set; }

        /// <summary>
        /// Application menu and restful APIs
        /// </summary>
        public Models.Database.StoredProcedures.SP_User_GetAccessMenu AccessMenu { get; set; }

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
            this.AccessMenu = AccessMenuResult.Data as Models.Database.StoredProcedures.SP_User_GetAccessMenu;

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
        public static DBResult Login(Logic.Database.IDBCTX ctx, Models.Database.StoredProcedures.SP_Login_Params filterParams)
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
    }
}
