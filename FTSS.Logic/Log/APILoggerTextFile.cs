using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    public class APILoggerTextFile : IAPILogger
    {
        private readonly FileIO.IFileOperation _fileOperation;

        public APILoggerTextFile(FileIO.IFileOperation fileOperation)
        {
            if (fileOperation == null)
                throw new ArgumentNullException("Invalid FileOperation");

            _fileOperation = fileOperation;
        }


        public APILoggerTextFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("Invalid path");

            _fileOperation = new FileIO.TextFile(path);
        }


        /// <summary>
        /// Log API at text file
        /// </summary>
        /// <param name="data"></param>
        public void Save(Models.API.Log data)
        {
            if (data == null)
                throw new ArgumentNullException("Invalid data");

            var json = Logic.CommonOperations.JSON.ObjToJson(data);

            _fileOperation.Append(json);
        }
    }
}
