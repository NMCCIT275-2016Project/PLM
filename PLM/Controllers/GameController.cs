using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLM.Controllers
{
    public class GameController : Controller
    {
        private PLMContext db = new PLMContext();
        private Module currentModule = new Module();
        private PlayViewModel currentGuess = new PlayViewModel();
        private bool PLMgenerated = false;
        private List<Answer> UserCompletedAnswers = new List<Answer>();
        private List<int> GeneratedGuessIDs = new List<int>();
        private Random rand = new Random(DateTime.Today.Second);
        private int guessID;
        private int answerID;
        // Needs to be 1 less than the number we want, because the correct answer
        // will also be included in the list, and this value is used to generate
        // the incorrect answers
        private int NumAnswersDifficultyBased = 2;
        private int WrongGuess;

        //Module currentModule;
        //
        // GET: /Game/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play(int? PLMid)
        {
            // Added the '?' after int to make the value optional
            // Need to figure out how to set an optional int to an int
            int IDtoPASS = 0;
            if (PLMid == null)
            {
                IDtoPASS = 1;
            }

            if (PLMgenerated == false)
                GenerateModule(IDtoPASS);

            GenerateGuess();
            return View(currentGuess);
        }

        public ActionResult Setup()
        {
            return View();
        }

        private void GenerateModule(int PLMid)
        {
            currentModule = db.Modules.Find(PLMid);
        }

        private void GenerateGuess()
        {
            guessID = rand.Next(0, (currentModule.Answers.Count - 1));
            answerID = rand.Next(0, (currentModule.Answers.ElementAt(guessID).Pictures.Count));


            currentGuess.Answer = currentModule.Answers.ElementAt(guessID).AnswerString;
            currentGuess.ImageURL = currentModule.Answers.ElementAt(guessID).Pictures.ElementAt(answerID).Location;
            currentGuess.possibleAnswers.Add(currentModule.Answers.ElementAt(guessID).AnswerString);
            GeneratedGuessIDs.Add(currentModule.Answers.ElementAt(guessID).AnswerID);

            GenerateWrongAnswers();
        }

        private void GenerateWrongAnswers()
        {
            WrongGuess = guessID;
            while (GeneratedGuessIDs.Contains(WrongGuess))
            {
                WrongGuess = rand.Next(0, (currentModule.Answers.Count - 1));
            }
            currentGuess.possibleAnswers.Add(currentModule.Answers.ElementAt(WrongGuess).AnswerString);
            GeneratedGuessIDs.Add(WrongGuess);
        }
    }
}