using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISW_GASISW.Models;

namespace ISW_GASISW.Controllers
{
    public class FormulariosController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Formularios/

        public ActionResult Index()
        {
            return View(db.formularios.ToList());
        }

        //
        // GET: /Formularios/Details/5

        public ActionResult Details(long id = 0)
        {
            formularios formularios = db.formularios.Find(id);
            if (formularios == null)
            {
                return HttpNotFound();
            }
            return View(formularios);
        }

        //
        // GET: /Formularios/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Formularios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(formularios formularios)
        {
            if (ModelState.IsValid)
            {
                db.formularios.Add(formularios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formularios);
        }

        //
        // GET: /Formularios/Edit/5

        public ActionResult Edit(long id = 0)
        {
            formularios formularios = db.formularios.Find(id);
            if (formularios == null)
            {
                return HttpNotFound();
            }
            return View(formularios);
        }

        //
        // POST: /Formularios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(formularios formularios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formularios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formularios);
        }

        //
        // GET: /Formularios/Delete/5

        public ActionResult Delete(long id = 0)
        {
            formularios formularios = db.formularios.Find(id);
            if (formularios == null)
            {
                return HttpNotFound();
            }
            return View(formularios);
        }

        //
        // POST: /Formularios/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            formularios formularios = db.formularios.Find(id);
            db.formularios.Remove(formularios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}