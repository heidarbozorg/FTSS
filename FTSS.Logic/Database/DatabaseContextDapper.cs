using FTSS.Models.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Database
{
    public class DatabaseContextDapper : IDatabaseContext
    {
        #region properties
        private string _connectionString { get; set; }
        private SqlConnection _sqlConnection { get; set; }

        public DatabaseContextDapper(string ConnectionString)
        {
            _connectionString = ConnectionString;
            _sqlConnection = new SqlConnection(_connectionString);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public SqlConnection GetSqlConnection()
        {
            return _sqlConnection;
        }
        #endregion properties

        #region SPs
        public DBResult SP_Login(Models.Database.StoredProcedures.SP_Login.Inputs inputs)
        {
            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Login(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_AccessToAPI(Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs inputs)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid input data.");

            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_AccessToAPI(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_Users_GetAll(Models.Database.StoredProcedures.SP_Users_GetAll.Inputs inputs)
        {
            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_Users_GetAll(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_ChangePassword(Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs inputs)
        {
            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_ChangePassword(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_UpdateProfile(Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs inputs)
        {
            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_UpdateProfile(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_SetPassword(Models.Database.StoredProcedures.SP_User_SetPassword.Inputs inputs)
        {
            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_SetPassword(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Delete(Models.Database.StoredProcedures.SP_User_Delete.Inputs inputs)
        {
            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Delete(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Update(Models.Database.StoredProcedures.SP_User_Update.Inputs inputs)
        {
            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Update(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }

        public DBResult SP_User_Insert(Models.Database.StoredProcedures.SP_User_Insert.Inputs inputs)
        {
            var connectionString = GetConnectionString();
            var sp = new FTSS.DP.DapperORM.StoredProcedure.SP_User_Insert(connectionString);
            var rst = sp.Call(inputs);
            return rst;
        }
        #endregion SPs
    }
}
