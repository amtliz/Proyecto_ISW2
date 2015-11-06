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
    public class AccesosController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Accesos/

        public ActionResult Index()
        {
            var accesos = db.accesos.Include(a => a.formularios).Include(a => a.permisos).Include(a => a.rol);
            return View(accesos.ToList());
        }

        //
        // GET: /Accesos/Details/5

        public ActionResult Details(long id = 0)
        {
            accesos accesos = db.accesos.Find(id);
            if (accesos == null)
            {
                return HttpNotFound();
            }
            return View(accesos);
        }

        //
        // GET: /Accesos/Create

        public ActionResult Create()
        {
            ViewBag.FORMULARIOS_id = new SelectList(db.formularios, "id", "nombre");
            ViewBag.PERMISOS_id = new SelectList(db.permisos, "id", "nombre");
            ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre");
            return View();
        }

        //
        // POST: /Accesos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(accesos accesos)
        {
            if (ModelState.IsValid)
            {
                db.accesos.Add(accesos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FORMULARIOS_id = new SelectList(db.formularios, "id", "nombre", accesos.FORMULARIOS_id);
            ViewBag.PERMISOS_id = new SelectList(db.permisos, "id", "nombre", accesos.PERMISOS_id);
            ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre", accesos.ROL_id);
            return View(accesos);
        }

        //
        // GET: /Accesos/Edit/5

        public ActionResult Edit(long id = 0)
        {
            accesos accesos = db.accesos.Find(id);
            if (accesos == null)
            {
                return HttpNotFound();
            }
            ViewBag.FORMULARIOS_id = new SelectList(db.formularios, "id", "nombre", accesos.FORMULARIOS_id);
            ViewBag.PERMISOS_id = new SelectList(db.permisos, "id", "nombre", accesos.PERMISOS_id);
            ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre", accesos.ROL_id);
            return View(accesos);
        }

        //
        // POST: /Accesos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(accesos accesos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accesos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FORMULARIOS_id = new SelectList(db.formularios, "id", "nombre", accesos.FORMULARIOS_id);
            ViewBag.PERMISOS_id = new SelectList(db.permisos, "id", "nombre", accesos.PERMISOS_id);
            ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre", accesos.ROL_id);
            return View(accesos);
        }

        //
        // GET: /Accesos/Delete/5

        public ActionResult Delete(long id = 0)
        {
            accesos accesos = db.accesos.Find(id);
            if (accesos == null)
            {
                return HttpNotFound();
            }
            return View(accesos);
        }

        //
        // POST: /Accesos/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            accesos accesos = db.accesos.Find(id);
            db.accesos.Remove(accesos);
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