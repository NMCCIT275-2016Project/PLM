using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLM.Controllers
{
    public class GameController : Controller
    {
        private static Random rand = new Random();

        private UserGameSession currentGameSession;
        private ApplicationDbContext db = new ApplicationDbContext();
        private Module currentModule = new Module();
        private List<int> GeneratedGuessIDs = new List<int>();
        private PlayViewModel currentGuess = new PlayViewModel();
        private int currentGuessNum;

        private bool PLMgenerated = false;
        private bool WrongAnswersGenerationNOTcompleted = true;

        private int answerID;
        private int pictureID;
        private int wrongAnswerID;
        private int NumAnswersDifficultyBased = 5;

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

            GenerateGuessONEperANS();
            return View(currentGuess);
        }

        [HttpPost]
        public ActionResult Play(int Score)
        {
            ((UserGameSession)Session["userGameSession"]).Score = Score;
            if (IsGameDone())
            {
                return RedirectToAction("Complete");
            }
            GenerateGuessONEperPIC();
            currentGuess.Score = Score;
            return View(currentGuess);
        }

        public ActionResult Setup()
        {
            return View();
        }

        private bool IsGameDone()
        {
            currentModule = ((UserGameSession)Session["userGameSession"]).currentModule;
            if (((UserGameSession)Session["userGameSession"]).currentGuess >= (((UserGameSession)Session["userGameSession"]).Pictures.Count))
            {
                return true;
            }
            else
                return false;
        }

        private void GenerateModule(int PLMid)
        {
            currentModule = db.Modules.Find(PLMid);
            // Turned shuffled off so that elementAt() works correctly
            // Shuffled the list of pictures instead so random generation
            // still works
            //currentModule.Answers.Shuffle();
            //foreach (Answer answer in currentModule.Answers)
            //{
            //    answer.Pictures.Shuffle();
            //}
            currentGameSession = new UserGameSession();
            currentGameSession.Score = 0;
            // set to -1 because GenerateGuess() will increment it to 0 the first time it runs
            currentGameSession.currentGuess = -1;
            currentGameSession.currentModule = currentModule;
            foreach (Answer answer in currentModule.Answers)
            {
                foreach (Picture picture in answer.Pictures)
                {
                    currentGameSession.Pictures.Add(picture);
                }
            }
            // shuffle pictures list so random pull still works
            currentGameSession.Pictures.Shuffle();
            Session["userGameSession"] = currentGameSession;
        }

        // Generates Guess, loops through each picture in each answer
        // the same answer will be chosen multiple times with different pictures
        private void GenerateGuessONEperPIC()
        {
            currentGuessNum = ((UserGameSession)Session["userGameSession"]).currentGuess++;
            currentModule = ((UserGameSession)Session["userGameSession"]).currentModule;
            pictureID = GetPictureID(currentGuessNum);
            answerID = GetAnswerID();

            currentGuess.Answer = currentModule.Answers.ElementAt(answerID).AnswerString;
            currentGuess.ImageURL = currentModule.Answers.ElementAt(answerID).Pictures.ElementAt(pictureID).Location;
            currentGuess.possibleAnswers.Add(currentModule.Answers.ElementAt(answerID).AnswerString);

            GeneratedGuessIDs.Add(answerID);
            GenerateWrongAnswers();

            currentGuess.possibleAnswers.Shuffle();
        }

        private int GetPictureID(int currentGuessNum)
        {
            Picture currentPicture = ((UserGameSession)Session["userGameSession"]).Pictures.ElementAt(currentGuessNum);

            foreach (Answer answer in currentModule.Answers)
            {
                foreach (Picture picture in answer.Pictures)
                {
                    if ((picture.PictureID == currentPicture.PictureID) && (answer.AnswerID == currentPicture.Answer.AnswerID))
                    {
                        return picture.PictureID;
                    }
                }
            }
            return 1;
        }

        private int GetAnswerID()
        {
            foreach (Answer answer in currentModule.Answers)
            {
                foreach (Picture picture in answer.Pictures)
                {
                    if (picture.PictureID == pictureID)
                    {
                        return answer.AnswerID;
                    }
                }
            }
            // Defaults to 1 so error doesn't occur
            return 1;
        }

        // Generates guess, only loops through each answer once, so only
        // one picture will be chosen per answer
        private void GenerateGuessONEperANS()
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
            GeneratedGuessIDs.Add(answerID);

            //Generate a random selection of wrong answers and add them to the possible answers.
            GenerateWrongAnswers();

            //shuffle the list of possible answers so that the first answer isn't always the right one.
            currentGuess.possibleAnswers.Shuffle();
        }

        private void GenerateWrongAnswers()
        {
            //while we still have work to do
            while (WrongAnswersGenerationNOTcompleted)
            {
                do
                {
                    wrongAnswerID = rand.Next(0, (currentModule.Answers.Count - 1));
                } while (GeneratedGuessIDs.Contains(wrongAnswerID));

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