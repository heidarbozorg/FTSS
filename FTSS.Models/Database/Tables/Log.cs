using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Models.Database
{
    public class Log
    {
        public int LogiId { get; set; }
        public string IPAddress { get; set; }
        public string MSG { get; set; }
        public DateTime Date { get; set; }
    }
}
