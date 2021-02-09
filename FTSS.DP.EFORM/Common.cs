using Microsoft.Data.SqlClient;
using System.Linq;

namespace FTSS.DP.EFORM
{
    public class Common
    {
        public static string getMessage(SqlParameter[] parameters)
        {
            var errorMessage = parameters.FirstOrDefault(a => a.ParameterName == "@OutStr");
            var msg = errorMessage == null ? "" : (string)(errorMessage.Value is string ? errorMessage.Value : "");
            return (msg);
        }

        public static int getCode(SqlParameter[] parameters)
        {
            var ErrorCode = parameters.FirstOrDefault(a => a.ParameterName == "@ErrorCode");
            var code = ErrorCode == null ? 0 : (int)(ErrorCode.Value is int ? ErrorCode.Value : 0);
            return (code);
        }

    }
}
