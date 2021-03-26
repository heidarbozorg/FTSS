using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FTSS.DP.DapperORM
{
    public class BaseSP<TInput, TOutput>
    {
        private readonly string _spName;
        private readonly ISqlExecuter _executer;
        private DynamicParameters dataParams;

        public BaseSP(string SPName, ISqlExecuter executer)
        {
            _spName = SPName;
            _executer = executer;
        }

        private IEnumerable<TOutput> GetAll(TInput inputs)
        {
            if (inputs == null)
                throw new ArgumentNullException("Invalid inputs data.");

            var t = inputs.GetType();
            var fields = t.GetProperties();
            dataParams = Common.GetErrorCodeAndErrorMessageParams();
            foreach (var f in fields)
                dataParams.Add(f.Name, f.GetValue(inputs));

            var result = _executer.Query<TOutput>(_spName, dataParams, System.Data.CommandType.StoredProcedure);
            return result;
        }

        public Models.Database.DBResult Single(TInput input)
        {
            var result = GetAll(input).FirstOrDefault();
            return Common.GetResult(dataParams, result);
        }

        public Models.Database.DBResult Query(TInput input)
        {
            var result = GetAll(input);
            return Common.GetResult(dataParams, result);
        }
    }
}