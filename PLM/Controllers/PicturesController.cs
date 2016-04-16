using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PLM;

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
                db.Pictures.Add(picture);
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
    }
}
