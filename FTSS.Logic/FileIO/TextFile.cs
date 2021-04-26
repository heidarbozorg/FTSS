using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FTSS.Logic.FileIO
{
    public class TextFile : IFileOperation, IDisposable
    {
        private System.IO.StreamWriter _writer;

        public TextFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("Invalid path.");

            _writer = System.IO.File.AppendText(path);
        }

        public TextFile(System.IO.StreamWriter f)
        {
            if (f == null)
                throw new ArgumentNullException("Invalud Writer.");

            _writer = f;
        }

        /// <summary>
        /// Add text to the end of file
        /// </summary>
        /// <param name="text"></param>
        public void Append(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("Invalid text.");

            _writer.WriteLine(text);
        }

        /// <summary>
        /// Dispose resources
        /// </summary>
        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
