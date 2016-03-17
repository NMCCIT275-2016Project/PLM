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
        private static Random rand = new Random();
        private int answerID;
        private int pictureID;
        private int NumAnswersDifficultyBased = 3;
        private int wrongAnswerID;
        private bool WrongAnswersGenerationNOTcompleted = true;

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
            answerID = rand.Next(0, (currentModule.Answers.Count - 1));
            pictureID = rand.Next(0, (currentModule.Answers.ElementAt(answerID).Pictures.Count - 1));

            currentGuess.Answer = currentModule.Answers.ElementAt(answerID).AnswerString;
            currentGuess.ImageURL = currentModule.Answers.ElementAt(answerID).Pictures.ElementAt(pictureID).Location;
            currentGuess.possibleAnswers.Add(currentModule.Answers.ElementAt(answerID).AnswerString);
            GeneratedGuessIDs.Add(currentModule.Answers.ElementAt(answerID).AnswerID);

            GenerateWrongAnswers();
            //shuffle the list of possible answers so that the first answer isn't always the right one.
            currentGuess.possibleAnswers.Shuffle();
        }

        private void GenerateWrongAnswers()
        {
            while (WrongAnswersGenerationNOTcompleted)
            {
                while (GeneratedGuessIDs.Contains(wrongAnswerID))
                {
                    wrongAnswerID = rand.Next(0, (currentModule.Answers.Count - 1));
                }
                currentGuess.possibleAnswers.Add(currentModule.Answers.ElementAt(wrongAnswerID).AnswerString);
                GeneratedGuessIDs.Add(wrongAnswerID);

                if (GeneratedGuessIDs.Count >= NumAnswersDifficultyBased)
                {
                    WrongAnswersGenerationNOTcompleted = false;
                }
            }
        }
    }
}