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
            p.Add("@ErrorCode", errorCode, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            p.Add("@ErrorMessage", errorMessage, System.Data.DbType.String, System.Data.ParameterDirection.Output);

            return p;
        }

        public static DynamicParameters GetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException("Token could not be empty.");

            var p = GetErrorCodeAndErrorMessageParams();
            p.Add("@Token", token);

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

            //Get token param
            var p = GetToken(filterParams.Token);

            //Pagination
            p.Add("@StartIndex", filterParams.StartIndex, System.Data.DbType.Int32);
            p.Add("@PageSize", filterParams.PageSize, System.Data.DbType.Byte);

            int actualSize = 0;
            p.Add("@ActualSize", actualSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);

            return p;
        }

        public static DynamicParameters GetDataParams(Models.Database.BaseDataModelWithToken data)
        {
            if (data == null)
                throw new ArgumentNullException("Invalid data");

            var p = GetToken(data.Token);
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
                
                StatusCode = GetOutputValue<int>(outputParams, "ErrorCode"),
                ErrorMessage = GetOutputValue<string>(outputParams, "ErrorMessage"),

                Data = data
            };

            try
            {
                //if database query for searching, try to catch ActualSize output param for pagination
                rst.ActualSize = outputParams.Get<int>("ActualSize");
            }
            catch (Exception)
            {
                rst.ActualSize = data == null ? 0 : 1;
            }

            return rst;
        }

    }
}
