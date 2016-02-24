using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PLM
{
    public class Module
    {
        private int _id;
        private string _name;
        private string _topic;
        private int _authorID;

        public int AuthorID
        {
            get { return _authorID; }
            set { _authorID = value; }
        }
        public string Topic
        {
            get { return _topic; }
            set { _topic = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}