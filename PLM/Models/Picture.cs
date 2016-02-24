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
        private string _answerID;
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string AnswerID
        {
            get { return _answerID; }
            set { _answerID = value; }
        }
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}