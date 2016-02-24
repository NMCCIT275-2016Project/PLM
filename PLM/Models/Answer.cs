using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PLM
{
    public class Answer
    {
        private int _id;
        private string _answer;
        private int _moduleID;

        public int ModuleID
        {
            get { return _moduleID; }
            set { _moduleID = value; }
        }
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}