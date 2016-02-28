using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PLM
{
    public class Module
    {
        public int ModuleID { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}