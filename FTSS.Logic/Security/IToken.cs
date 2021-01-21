using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Security
{
    public interface IToken<T>
    {
        /// <summary>
        /// Generate token from Database object
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        string GenerateToken(T Data);

        /// <summary>
        /// Catch info from token
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        T GetData(string Token);
    }
}
