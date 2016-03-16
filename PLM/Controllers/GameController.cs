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

            GenerateModule(IDtoPASS);
            return View(currentModule);
        }

        public ActionResult Setup()
        {
            return View();
        }

        private void GenerateModule(int PLMid)
        {
            currentModule = db.Modules.Find(PLMid);
        }
    }
}