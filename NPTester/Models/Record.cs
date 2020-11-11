using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPTester.Models
{
    public class Record
    {
        public string TimeToConnect { get; set; }
        public string DBName { get; set; }
        public string DBConnectionResult { get; set; }
        public string DBName2 { get; set; }
        public string DBConnectionResult2 { get; set; }
        public string TimeToConnect2 { get; set; }
        public string DebugBuffer { get; set; }

    }
}