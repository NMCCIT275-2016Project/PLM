﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLM
{
    public class PlayViewModel
    {
        public string ImageURL;
        public List<string> possibleAnswers;
        public string Answer;

        public PlayViewModel()
        {
            possibleAnswers = new List<string>();
        }
    }
}