using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FTSS.DP.DapperORM
{
    public interface ISqlExecuter
    {
        /// <summary>
        /// Execute database query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null);
    }
}
