using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM
{
    public interface ISQLExecuter
    {
        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, System.Data.CommandType? commandType = null);
    }
}
