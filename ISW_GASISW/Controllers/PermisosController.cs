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
    public class PermisosController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Permisos/

        public ActionResult Index()
        {
            return View(db.permisos.ToList());
        }

        //
        // GET: /Permisos/Details/5

        public ActionResult Details(long id = 0)
        {
            permisos permisos = db.permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        //
        // GET: /Permisos/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Permisos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.permisos.Add(permisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permisos);
        }

        //
        // GET: /Permisos/Edit/5

        public ActionResult Edit(long id = 0)
        {
            permisos permisos = db.permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        //
        // POST: /Permisos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permisos);
        }

        //
        // GET: /Permisos/Delete/5

        public ActionResult Delete(long id = 0)
        {
            permisos permisos = db.permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        //
        // POST: /Permisos/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            permisos permisos = db.permisos.Find(id);
            db.permisos.Remove(permisos);
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