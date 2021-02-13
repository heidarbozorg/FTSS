using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ConnectionString"></param>
        public DatabaseContextDapper(string ConnectionString)
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentNullException("ConnectionString could not be empty for creating DapperCRM.");

            _connectionString = ConnectionString;
        }


        #region SPs
        public DBResult SP_Log_Insert(Models.Database.StoredProcedures.SP_Log_Insert.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_Log_Insert.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new DP.DapperORM.StoredProcedure.SP_Log_Insert(connectionString);

            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_APILog_Insert(Models.Database.StoredProcedures.SP_APILog_Insert.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_APILog_Insert.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_APILog_Insert(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_Login(Models.Database.StoredProcedures.SP_Login.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_Login.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Login(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_AccessToAPI(Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_AccessToAPI(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_Users_GetAll(Models.Database.StoredProcedures.SP_Users_GetAll.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_Users_GetAll.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Users_GetAll(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_ChangePassword(Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_ChangePassword(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_UpdateProfile(Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_UpdateProfile(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_SetPassword(Models.Database.StoredProcedures.SP_User_SetPassword.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_SetPassword.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_SetPassword(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Delete(Models.Database.StoredProcedures.SP_User_Delete.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_Delete.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Delete(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Update(Models.Database.StoredProcedures.SP_User_Update.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_Update.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Update(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Insert(Models.Database.StoredProcedures.SP_User_Insert.Inputs inputs,
            ISP<Models.Database.StoredProcedures.SP_User_Insert.Inputs> sp = null)
        {
            var connectionString = GetConnectionString();
            if (sp == null)
                sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Insert(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }
        #endregion SPs
    }
}