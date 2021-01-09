using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FTSS.Logic.FileIO
{
    public class TextFile : IFileOperation, IDisposable
    {
        private System.IO.StreamWriter _file;

        public TextFile(string path, System.IO.StreamWriter f = null)
        {
            if (f == null)
                _file = System.IO.File.AppendText(path);
            else
                _file = f;
        }

        /// <summary>
        /// Add text to the end of file
        /// </summary>
        /// <param name="text"></param>
        public void Append(string text)
        {
            _file.WriteLine(text);
        }

        /// <summary>
        /// Dispose resources
        /// </summary>
        public void Dispose()
        {
            _file.Dispose();
        }
    }
}
