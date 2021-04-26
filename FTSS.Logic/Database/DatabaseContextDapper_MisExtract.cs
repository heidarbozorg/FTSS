using FTSS.Models.Database;
using FTSS.Models.Database.StoredProcedures;
using System;

namespace FTSS.Logic.Database
{
    /// <summary>
    /// Implement IDatabaseContext by Dapper ORM
    /// </summary>
    public class DatabaseContextDapper_MisExtract : IDatabaseContext_MisExtract
    {
        #region properties
        private string _connectionString { get; set; }

        private string GetConnectionString()
        {
            return _connectionString;
        }
        #endregion properties

        private readonly DP.DapperORM.BaseSP<SP_APILog_Insert.Inputs, SP_APILog_Insert.Outputs> _SP_APILog_Insert;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ConnectionString"></param>
        public DatabaseContextDapper_MisExtract(string ConnectionString, DP.DapperORM.ISqlExecuter executer = null)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException("رشته اتصال یافت نشد!");

            _connectionString = ConnectionString;
            if (executer == null)
                executer = new DP.DapperORM.SqlExecuter(GetConnectionString());
            _SP_APILog_Insert = new DP.DapperORM.BaseSP<SP_APILog_Insert.Inputs, SP_APILog_Insert.Outputs>("SP_APILog_Insert", executer);
          
        }
        #region SPs
        public DBResult SP_APILog_Insert(SP_APILog_Insert.Inputs inputs)
        {
            var rst = _SP_APILog_Insert.Single(inputs);
            return rst;
        }
        public DBResult SP_AmalkardKarkhanehStop5(SP_APILog_Insert.Inputs inputs)
        {
            var rst = _SP_APILog_Insert.Single(inputs);
            return rst;
        }


        #endregion SPs
    }
}