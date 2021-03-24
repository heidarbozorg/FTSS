using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FTSS.DP.DapperORM
{
    public class BaseSP<TInput, TOutput>
    {
        private readonly string _spName;
        private readonly ISQLExecuter _executer;

        public BaseSP(string SPName, ISQLExecuter executer)
        {
            _spName = SPName;
            _executer = executer;
        }

        public Models.Database.DBResult Single(TInput input)
        {
            if (input == null)
                throw new ArgumentNullException("Invalid inputs data.");

            var t = input.GetType();
            var fields = t.GetProperties();
            var data = Common.GetErrorCodeAndErrorMessageParams();
            foreach (var f in fields)
                data.Add(f.Name, f.GetValue(input));

            var result = _executer.Query<TOutput>(_spName, data, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            return Common.GetResult(data, result);
        }
    }
}