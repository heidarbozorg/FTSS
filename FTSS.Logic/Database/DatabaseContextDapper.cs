using FTSS.Models.Database;
using FTSS.Models.Database.StoredProcedures;
using System;

namespace FTSS.Logic.Database
{
    /// <summary>
    /// Implement IDatabaseContext by Dapper ORM
    /// </summary>
    public class DatabaseContextDapper : IDatabaseContext
    {
        #region properties
        private string _connectionString { get; set; }

        private string GetConnectionString()
        {
            return _connectionString;
        }
        #endregion properties

        private readonly DP.DapperORM.BaseSP<SP_Admin_Login.Inputs, SP_Admin_Login.Outputs> _SP_Admin_Login;
        private readonly DP.DapperORM.BaseSP<SP_APILog_Insert.Inputs, SP_APILog_Insert.Outputs> _SP_APILog_Insert;
        private readonly DP.DapperORM.BaseSP<SP_User_AccessToAPI.Inputs, SP_User_AccessToAPI.Outputs> _SP_User_AccessToAPI;
        private readonly DP.DapperORM.BaseSP<SP_Users_GetAll.Inputs, SP_Users_GetAll.Outputs> _SP_Users_GetAll;
        private readonly DP.DapperORM.BaseSP<SP_User_ChangePassword.Inputs, SP_User_ChangePassword.Outputs> _SP_User_ChangePassword;
        private readonly DP.DapperORM.BaseSP<SP_User_UpdateProfile.Inputs, SP_User_UpdateProfile.Outputs> _SP_User_UpdateProfile;
        private readonly DP.DapperORM.BaseSP<SP_User_SetPassword.Inputs, SP_User_SetPassword.Outputs> _SP_User_SetPassword;
        private readonly DP.DapperORM.BaseSP<SP_User_Delete.Inputs, SP_User_Delete.Outputs> _SP_User_Delete;
        private readonly DP.DapperORM.BaseSP<SP_User_Update.Inputs, SP_User_Update.Outputs> _SP_User_Update;
        private readonly DP.DapperORM.BaseSP<SP_User_Insert.Inputs, SP_User_Insert.Outputs> _SP_User_Insert;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ConnectionString"></param>
        public DatabaseContextDapper(string ConnectionString, DP.DapperORM.ISqlExecuter executer = null)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException("ConnectionString could not be empty for creating DapperCRM.");

            _connectionString = ConnectionString;
            if (executer == null)
                executer = new DP.DapperORM.SqlExecuter(GetConnectionString());
            _SP_Admin_Login = new DP.DapperORM.BaseSP<SP_Admin_Login.Inputs, SP_Admin_Login.Outputs>("SP_Admin_Login", executer);
            _SP_APILog_Insert = new DP.DapperORM.BaseSP<SP_APILog_Insert.Inputs, SP_APILog_Insert.Outputs>("SP_APILog_Insert", executer);
            _SP_User_AccessToAPI = new DP.DapperORM.BaseSP<SP_User_AccessToAPI.Inputs, SP_User_AccessToAPI.Outputs>("SP_User_AccessToAPI", executer);
            _SP_Users_GetAll = new DP.DapperORM.BaseSP<SP_Users_GetAll.Inputs, SP_Users_GetAll.Outputs>("SP_Users_GetAll", executer);
            _SP_User_ChangePassword = new DP.DapperORM.BaseSP<SP_User_ChangePassword.Inputs, SP_User_ChangePassword.Outputs>("SP_User_ChangePassword", executer);
            _SP_User_UpdateProfile = new DP.DapperORM.BaseSP<SP_User_UpdateProfile.Inputs, SP_User_UpdateProfile.Outputs>("SP_User_UpdateProfile", executer);
            _SP_User_SetPassword = new DP.DapperORM.BaseSP<SP_User_SetPassword.Inputs, SP_User_SetPassword.Outputs>("SP_User_SetPassword", executer);
            _SP_User_Delete = new DP.DapperORM.BaseSP<SP_User_Delete.Inputs, SP_User_Delete.Outputs>("SP_User_Delete", executer);
            _SP_User_Update = new DP.DapperORM.BaseSP<SP_User_Update.Inputs, SP_User_Update.Outputs>("SP_User_Update", executer);
            _SP_User_Insert = new DP.DapperORM.BaseSP<SP_User_Insert.Inputs, SP_User_Insert.Outputs>("SP_User_Insert", executer);
        }


        #region SPs
        public DBResult SP_APILog_Insert(SP_APILog_Insert.Inputs inputs)
        {
            var rst = _SP_APILog_Insert.Single(inputs);
            return rst;
        }

        public DBResult SP_Admin_Login(SP_Admin_Login.Inputs inputs)
        {  
            var rst = _SP_Admin_Login.Single(inputs);
            return rst;
        }

        public DBResult SP_User_AccessToAPI(SP_User_AccessToAPI.Inputs inputs)
        {
            var rst = _SP_User_AccessToAPI.Single(inputs);
            return rst;
        }

        public DBResult SP_Users_GetAll(SP_Users_GetAll.Inputs inputs)
        {
            var rst = _SP_Users_GetAll.Query(inputs);
            return rst;
        }

        public DBResult SP_User_ChangePassword(SP_User_ChangePassword.Inputs inputs)
        {
            var rst = _SP_User_ChangePassword.Single(inputs);
            return rst;
        }

        public DBResult SP_User_UpdateProfile(SP_User_UpdateProfile.Inputs inputs)
        {
            var rst = _SP_User_UpdateProfile.Single(inputs);
            return rst;
        }

        public DBResult SP_User_SetPassword(SP_User_SetPassword.Inputs inputs)
        {
            var rst = _SP_User_SetPassword.Single(inputs);
            return rst;
        }

        public DBResult SP_User_Delete(SP_User_Delete.Inputs inputs)
        {
            var rst = _SP_User_Delete.Single(inputs);
            return rst;
        }

        public DBResult SP_User_Update(SP_User_Update.Inputs inputs)
        {
            var rst = _SP_User_Update.Single(inputs);
            return rst;
        }

        public DBResult SP_User_Insert(SP_User_Insert.Inputs inputs)
        {
            var rst = _SP_User_Insert.Single(inputs);
            return rst;
        }
        #endregion SPs
    }
}