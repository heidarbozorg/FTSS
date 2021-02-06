using System.Collections.Generic;
using System.Data;

namespace FTSS.DP.DapperORM
{
    public interface ISQLExecuter
    {
        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null);
    }
}
