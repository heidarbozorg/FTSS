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

        private readonly DP.DapperORM.BaseSP<SP_Login.Inputs, SP_Login.Outputs> _SP_Login;
        private readonly DP.DapperORM.BaseSP<SP_APILog_Insert.Inputs, SP_APILog_Insert.Outputs> _SP_APILog_Insert;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ConnectionString"></param>
        public DatabaseContextDapper(string ConnectionString, DP.DapperORM.ISQLExecuter executer = null)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException("ConnectionString could not be empty for creating DapperCRM.");

            _connectionString = ConnectionString;
            if (executer == null)
                executer = new DP.DapperORM.SQLExecuter(GetConnectionString());
            _SP_Login = new DP.DapperORM.BaseSP<SP_Login.Inputs, SP_Login.Outputs>("SP_Login", executer);
            _SP_APILog_Insert = new DP.DapperORM.BaseSP<SP_APILog_Insert.Inputs, SP_APILog_Insert.Outputs>("SP_APILog_Insert", executer);
        }


        #region SPs
        public DBResult SP_APILog_Insert(SP_APILog_Insert.Inputs inputs)
        {
            if (string.IsNullOrEmpty(inputs.APIAddress))
                throw new ArgumentNullException("APIAddress could not be empty.");

            var rst = _SP_APILog_Insert.Single(inputs);
            return rst;
        }

        public DBResult SP_Login(SP_Login.Inputs inputs)
        {  
            if (string.IsNullOrEmpty(inputs.Email) || string.IsNullOrEmpty(inputs.Password))
                throw new ArgumentNullException("Email and Password could not be empty.");
            
            var rst = _SP_Login.Single(inputs);
            return rst;
        }

        public DBResult SP_User_AccessToAPI(Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs> sp = null)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            if (string.IsNullOrEmpty(inputs.Token) || string.IsNullOrEmpty(inputs.APIAddress))
                throw new ArgumentNullException("Token and APIAddress could not be empty.");

            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_AccessToAPI(GetConnectionString());
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_Users_GetAll(Models.Database.StoredProcedures.SP_Users_GetAll.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_Users_GetAll.Inputs> sp = null)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            if (!inputs.IsValid())
                throw new ArgumentException("Invalid data.");

            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Users_GetAll(GetConnectionString());
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_ChangePassword(Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs> sp = null)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            if (!inputs.IsValid())
                throw new ArgumentException("Invalid input data.");

            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_ChangePassword(GetConnectionString());
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_UpdateProfile(Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs> sp = null)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            if (!inputs.IsValid())
                throw new ArgumentException("Invalid input data.");

            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_UpdateProfile(GetConnectionString());
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_SetPassword(Models.Database.StoredProcedures.SP_User_SetPassword.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_SetPassword.Inputs> sp = null)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            if (!inputs.IsValid())
                throw new ArgumentException("Invalid input data.");

            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_SetPassword(GetConnectionString());
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Delete(Models.Database.StoredProcedures.SP_User_Delete.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_Delete.Inputs> sp = null)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            if (!inputs.IsValid())
                throw new ArgumentException("Invalid input data.");

            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Delete(GetConnectionString());
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Update(Models.Database.StoredProcedures.SP_User_Update.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_Update.Inputs> sp = null)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            if (!inputs.IsValid())
                throw new ArgumentException("Invalid input data.");

            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Update(GetConnectionString());
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Insert(Models.Database.StoredProcedures.SP_User_Insert.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_Insert.Inputs> sp = null)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            if (!inputs.IsValid())
                throw new ArgumentException("Invalid input data.");

            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Insert(GetConnectionString());
            var rst = sp.Call(inputs);
            return rst;
        }
        #endregion SPs
    }
}