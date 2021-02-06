using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.DP.DapperORM
{
    public class Common
    {
        public static DynamicParameters GetSearchParams()
        {
            var p = new DynamicParameters();
            int errorCode = 0;
            string errorMessage = "";
            p.Add("@ErrorCode", errorCode, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            p.Add("@ErrorMessage", errorMessage, System.Data.DbType.String, System.Data.ParameterDirection.Output);

            return p;
        }

        public static DynamicParameters GetSearchParams(string token)
        {
            var p = GetSearchParams();
            p.Add("@Token", token);

            return p;
        }

        public static DynamicParameters GetSearchParams(Models.Database.BaseSearchParams filterParams)
        {
            var p = GetSearchParams(filterParams.Token);
            
            //Pagination
            p.Add("@StartIndex", filterParams.StartIndex, System.Data.DbType.Int32);
            p.Add("@PageSize", filterParams.PageSize, System.Data.DbType.Int32);

            int actualSize = 0;
            p.Add("@ActualSize", actualSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            return p;
        }

        public static DynamicParameters GetDataParams(Models.Database.BaseDataModelWithToken data)
        {
            var p = GetSearchParams(data.Token);
            return p;
        }



        /// <summary>
        /// Convert database query result to DBResult type
        /// </summary>
        /// <param name="p"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.Database.DBResult GetResult(DynamicParameters p, object data)
        {
            var rst = new Models.Database.DBResult()
            {
                ErrorCode = p.Get<int>("ErrorCode"),
                ErrorMessage = p.Get<string>("ErrorMessage"),

                Data = data
            };

            try
            {
                //if database query for searching, try to catch ActualSize output param for pagination
                rst.ActualSize = p.Get<int>("ActualSize");
            }
            catch (Exception)
            {
                rst.ActualSize = data == null ? 0 : 1;
            }

            return rst;
        }

    }
}
