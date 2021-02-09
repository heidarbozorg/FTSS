using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.CommonOperations
{
    /// <summary>
    /// Implementing convert object to json and json to object operations
    /// </summary>
    public class JSON
    {
        /// <summary>
        /// Convert json to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T jsonToT<T>(string json)
        {
            var rst = JsonConvert.DeserializeObject<T>(json);
            return (rst);
        }

        /// <summary>
        /// Convert object to json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ObjToJson(object data)
        {
            var rst = JsonConvert.SerializeObject(data);
            return rst;
        }
    }
}
