using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.DP.DapperORM
{
    public class Common
    {
        public static DynamicParameters GetEmptyParams()
        {
            var p = new DynamicParameters();
            return p;
        }

        public static DynamicParameters GetErrorCodeAndErrorMessageParams()
        {
            var p = GetEmptyParams();
            int errorCode = 0;
            string errorMessage = "";
            p.Add("@OutStr", errorCode, System.Data.DbType.String, System.Data.ParameterDirection.Output);
            p.Add("@ErrorCode", errorMessage, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            return p;
        }

        public static DynamicParameters GetWAPIToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("Token could not be empty.");

            var p = GetErrorCodeAndErrorMessageParams();
            p.Add("@WAPIToken", token);

            return p;
        }

        public static DynamicParameters GetSearchParams(Models.Database.BaseSearchParams filterParams)
        {
            if (filterParams == null)
                throw new ArgumentNullException("Invalid params.");

            if (filterParams.StartIndex < 0)
                throw new ArgumentOutOfRangeException("StartIndex could not be less than zero.");

            if (filterParams.PageSize < 1)
                throw new ArgumentOutOfRangeException("PageSize could not be less than one.");
            var p = new DynamicParameters() ;
            //Pagination
            p.Add("@StartIndex", filterParams.StartIndex, System.Data.DbType.Int32);
            p.Add("@PageSize", filterParams.PageSize, System.Data.DbType.Byte);

            int actualSize = 0;
            p.Add("@ActualSize", actualSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            return p;
        }



        private static T GetOutputValue<T>(DynamicParameters outputParams, string key)
        {
            try
            {
                return outputParams.Get<T>(key);
            }
            catch (Exception)
            {
                return default(T);
            }
        }


        /// <summary>
        /// Convert database query result to DBResult type
        /// </summary>
        /// <param name="outputParams"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Models.Database.DBResult GetResult(DynamicParameters outputParams, object data)
        {
            if (outputParams == null)
                throw new ArgumentNullException("Invalid outputParams");

            var rst = new Models.Database.DBResult()
            {
                //ErrorCode = outputParams.Get<int>("ErrorCode"),
                //ErrorMessage = outputParams.Get<string>("ErrorMessage"),
                
                StatusCode = GetOutputValue<int>(outputParams, "OutStr"),
                ErrorMessage = GetOutputValue<string>(outputParams, "ErrorCode"),

                Data = data
            };
            if (data != null)
                rst.StatusCode = 200;


            return rst;
        }

    }
}
