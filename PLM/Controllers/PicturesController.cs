using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PLM;
using System.IO;

namespace PLM.Controllers
{
    public class PicturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Pictures/
        public ActionResult Index()
        {
            var pictures = db.Pictures.Include(p => p.Answer);
            return View(pictures.ToList());
        }

        // GET: /Pictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // GET: /Pictures/Create
        public ActionResult Create()
        {
            ViewBag.AnswerID = new SelectList(db.Answers, "AnswerID", "AnswerString");
            return View();
        }

        // POST: /Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PictureID,Location,AnswerID")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                picture.Location = "empty";
                db.Pictures.Add(picture);

                var location = "";
                if (location == "")
                {
                //error
                }
                else
                {
                    picture.Location = location;
                }

                db.SaveChanges();
                return RedirectToAction("edit", new { controller = "Answers", id = picture.AnswerID });
            }

            ViewBag.AnswerID = new SelectList(db.Answers, "AnswerID", "AnswerString", picture.AnswerID);
            return View(picture);
        }

        // GET: /Pictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnswerID = new SelectList(db.Answers, "AnswerID", "AnswerString", picture.AnswerID);
            return View(picture);
        }

        // POST: /Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PictureID,Location,AnswerID")] Picture picture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(picture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("edit", new { controller = "Answers", id = picture.AnswerID });
            }
            ViewBag.AnswerID = new SelectList(db.Answers, "AnswerID", "AnswerString", picture.AnswerID);
            return View(picture);
        }

        // GET: /Pictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return HttpNotFound();
            }
            return View(picture);
        }

        // POST: /Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Picture picture = db.Pictures.Find(id);
            db.Pictures.Remove(picture);
            db.SaveChanges();
            return RedirectToAction("edit", new { controller = "Answers", id = picture.AnswerID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public ActionResult SaveUploadedFile()
        {
            Session["upload"] = "AmerGeoPhoto";

            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        string moduleDirectory = (Path.Combine(Server.MapPath("~/Content/Images/PLM/" + Session["upload"].ToString() + "/" )));
                        if (!Directory.Exists(moduleDirectory)) 
                        {
                            Directory.CreateDirectory(moduleDirectory);
                        }
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Content\\Images\\PLM\\", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), moduleDirectory);

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);


                        var path = string.Format("{0}\\{1}", pathString, file.FileName);


                        file.SaveAs(path);

                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

    }
}
