using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace PLM
{
    public class Module
    {
        //private List<Picture> _pictures;
        private string _moduleLocation = "Images";
        private List<string> _wrongAnswers;

        public string ModuleLocation
        {
            get { return _moduleLocation; }
            set { _moduleLocation = value; }
        }
        
        public List<string> WrongAnswers
        {
            get { return _wrongAnswers; }
            set { _wrongAnswers = value; }
        }

        public List<Picture> Pictures;
        //public List<Picture> Pictures
        //{
        //    get { return _pictures; }
        //    set { _pictures = value; }
        //}

        public Module()
        {
            Pictures = new List<Picture>();
            _wrongAnswers = new List<string>();
            //AddPictures();
        }

        // Overloaded to add pictures specific to a module
        public Module(string module)
        {
            _moduleLocation = module;
            Pictures = new List<Picture>();
            _wrongAnswers = new List<string>();
            //AddPictures();
        }

        // Creates and adds pictures of a module and formats the properties
        // of each picture for the game to handle
        private void AddPictures()
        {
            string[] files = Directory.GetFiles(_moduleLocation, "*.png");

            foreach (string location in files)
            {
                //_pictures.Add(new Picture(location, Path.GetFileNameWithoutExtension(location)));
            }
        }

        // Using to get a base game working
        public void AddPicturesToList(Picture pic)
        {
            Pictures.Add(pic);
        }
        public void AddWrongAnswerToList(String answer)
        {
            WrongAnswers.Add(answer);
        }
    }
}