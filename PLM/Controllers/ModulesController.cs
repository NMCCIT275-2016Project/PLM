using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLM.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Modules
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddModule()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddModule(FormCollection form)
        {
            if (form["operation"] == "Add")
            {
                return Redirect("~/Modules/AddImage");
            }

            if (form["operation"] == "Edit")
            {
                return Redirect("~/Modules/EditImage");
            }

            if (form["operation"] == "Remove")
            {
                return Redirect("~/Modules/RemoveImage");
            }

            if (form["operation"] == "Save")
            {
                return Redirect("~/Modules/SaveModule");
            }
            else
            {
                return Redirect("/Modules/AddModule");
            }   
        }

        public ActionResult AddImage()
        {
            return View();
        }

        public ActionResult EditImage()
        {
            return View();
        }

        public ActionResult RemoveImage()
        {
            return View();
        }

        public ActionResult SaveModule()
        {
            return View();
        }
    }
}