using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLM.Controllers
{
    public class GameController : Controller
    {
        Module currentModule;
        //
        // GET: /Game/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play()
        {
            GenerateModule();

            return View(currentModule);
        }

        public ActionResult Setup()
        {
            return View();
        }

        private void GenerateModule()
        {
            currentModule = new Module();
            Picture pic1 = new Picture("https://cloud.githubusercontent.com/assets/16091910/12891557/83df8506-ce56-11e5-88c2-2fc434b476fc.jpg", "answer 1");
            Picture pic2 = new Picture("https://cloud.githubusercontent.com/assets/16091910/12891556/83df5a40-ce56-11e5-8198-45705d2127a2.jpg", "answer 2");
            Picture pic3 = new Picture("https://cloud.githubusercontent.com/assets/16091910/12891553/83da2f34-ce56-11e5-99c1-69f4e1c98fe9.jpg", "answer 3");
            currentModule.AddPicturesToList(pic1);
            currentModule.AddPicturesToList(pic2);
            currentModule.AddPicturesToList(pic3);
        }
	}
}