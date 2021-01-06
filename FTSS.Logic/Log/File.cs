using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.Log
{
    public class File : ILog
    {
        public File()
        {
            var f = new System.IO.StreamWriter("");
            var f2 = new System.IO.FileStream("", System.IO.FileAccess.ReadWrite);
            f2.Seek(0, System.IO.SeekOrigin.End);
        }


        public void Add(string msg, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Add(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
