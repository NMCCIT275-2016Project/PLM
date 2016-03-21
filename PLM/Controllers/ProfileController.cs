using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLM.Controllers
{
    public class ProfileController : Controller
    {
        private PLMContext db = new PLMContext();
        // GET: Profile
        public ActionResult Index()
        {
            return View(db.Modules.ToList());
        }
    }
}