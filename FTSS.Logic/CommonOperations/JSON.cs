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
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException("Invalid json");
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
            if (data == null)
                throw new ArgumentNullException("Invalid data");

            var rst = JsonConvert.SerializeObject(data);
            return rst;
        }        
        public static string get(string Name)
        {
            return Name;
            
        }
    }
}
