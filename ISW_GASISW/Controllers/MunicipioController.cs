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
    public class MunicipioController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Municipio/

        public ActionResult Index()
        {
            var municipio = db.municipio.Include(m => m.departamento);
            return View(municipio.ToList());
        }

        //
        // GET: /Municipio/Details/5

        public ActionResult Details(long id = 0)
        {
            municipio municipio = db.municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        //
        // GET: /Municipio/Create

        public ActionResult Create()
        {
            ViewBag.DEPARTAMENTO_id = new SelectList(db.departamento, "id", "nombre");
            return View();
        }

        //
        // POST: /Municipio/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.municipio.Add(municipio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DEPARTAMENTO_id = new SelectList(db.departamento, "id", "nombre", municipio.DEPARTAMENTO_id);
            return View(municipio);
        }

        //
        // GET: /Municipio/Edit/5

        public ActionResult Edit(long id = 0)
        {
            municipio municipio = db.municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            ViewBag.DEPARTAMENTO_id = new SelectList(db.departamento, "id", "nombre", municipio.DEPARTAMENTO_id);
            return View(municipio);
        }

        //
        // POST: /Municipio/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(municipio municipio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DEPARTAMENTO_id = new SelectList(db.departamento, "id", "nombre", municipio.DEPARTAMENTO_id);
            return View(municipio);
        }

        //
        // GET: /Municipio/Delete/5

        public ActionResult Delete(long id = 0)
        {
            municipio municipio = db.municipio.Find(id);
            if (municipio == null)
            {
                return HttpNotFound();
            }
            return View(municipio);
        }

        //
        // POST: /Municipio/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            municipio municipio = db.municipio.Find(id);
            db.municipio.Remove(municipio);
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