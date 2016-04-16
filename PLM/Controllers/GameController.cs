using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLM.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Module currentModule = new Module();
        private PlayViewModel currentGuess = new PlayViewModel();
        private bool PLMgenerated = false;
        private List<int> GeneratedGuessIDs = new List<int>();
        private static Random rand = new Random();
        private int answerID;
        private int pictureID;
        private int NumAnswersDifficultyBased = 3;
        private int wrongAnswerID;
        private bool WrongAnswersGenerationNOTcompleted = true;
        private UserGameSession currentGameSession;

        //Module currentModule;
        //
        // GET: /Game/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Complete()
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

        [HttpPost]
        public ActionResult Play()
        {
            if (IsGameDone())
            {
                return RedirectToAction("Complete");
            }
            GenerateGuess();
            return View(currentGuess);
        }

        public ActionResult Setup()
        {
            return View();
        }

        private bool IsGameDone()
        {
            currentModule = ((UserGameSession)Session["userGameSession"]).currentModule;
            if (((UserGameSession)Session["userGameSession"]).currentGuess >= currentModule.Answers.Count)
            {
                return true;
            }
            else
                return false;
        }

        private void GenerateModule(int PLMid)
        {
            currentModule = db.Modules.Find(PLMid);
            currentModule.Answers.Shuffle();
            currentGameSession = new UserGameSession();
            currentGameSession.Score = 0;
            // set to -1 because GenerateGuess() will increment it to 0 the first time it runs
            currentGameSession.currentGuess = -1;
            currentGameSession.currentModule = currentModule;

            Session["userGameSession"] = currentGameSession;
        }

        private void GenerateGuess()
        {
            ((UserGameSession)Session["userGameSession"]).currentGuess++;
            currentModule = ((UserGameSession)Session["userGameSession"]).currentModule;
            answerID = ((UserGameSession)Session["userGameSession"]).currentGuess;
            pictureID = rand.Next(0, (currentModule.Answers.ElementAt(answerID).Pictures.Count - 1));

            //add the initial stuff to the guess to send over
            currentGuess.Answer = currentModule.Answers.ElementAt(answerID).AnswerString;
            currentGuess.ImageURL = currentModule.Answers.ElementAt(answerID).Pictures.ElementAt(pictureID).Location;
            currentGuess.possibleAnswers.Add(currentModule.Answers.ElementAt(answerID).AnswerString);

            //add the correct answer to the generated guess ids (to prevent duplicate entries)
            GeneratedGuessIDs.Add(currentModule.Answers.ElementAt(answerID).AnswerID);

            //Generate a random selection of wrong answers and add them to the possible answers.
            GenerateWrongAnswers();
            //shuffle the list of possible answers so that the first answer isn't always the right one.
            currentGuess.possibleAnswers.Shuffle();
        }

        private void GenerateWrongAnswers()
        {
            wrongAnswerID = answerID;
            //while we still have work to do
            while (WrongAnswersGenerationNOTcompleted)
            {
                //for as long as the selected answer is a duplicate
                while (GeneratedGuessIDs.Contains(wrongAnswerID))
                {
                    //get a new, randomly selected answer
                    wrongAnswerID = rand.Next(0, (currentModule.Answers.Count - 1));
                }
                //add the selected answer to both the stuff to send over and the list of no longer addable answers
                currentGuess.possibleAnswers.Add(currentModule.Answers.ElementAt(wrongAnswerID).AnswerString);
                GeneratedGuessIDs.Add(wrongAnswerID);

                //if we've completed our work
                // TODO - Add functionality that checks if the module has enough answers to reach
                // the value of NumAnswersDifficultBased so that an error isn't thrown
                if (GeneratedGuessIDs.Count >= NumAnswersDifficultyBased)
                {
                    //break out of the loop
                    WrongAnswersGenerationNOTcompleted = false;
                }
            }
        }
    }
}