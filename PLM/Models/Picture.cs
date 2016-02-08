using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PLM
{
    public class Picture
    {
        private string _location;
        private string _answer;

        public string Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public Picture()
        {

        }
        public Picture(string location, string answer)
        {
            this._location = location;
            this._answer = answer;
        }
    }
}