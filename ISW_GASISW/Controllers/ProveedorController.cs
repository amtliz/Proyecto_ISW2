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
    public class ProveedorController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Proveedor/

        public ActionResult Index()
        {
            var proveedor = db.proveedor.Include(p => p.municipio);
            return View(proveedor.ToList());
        }

        //
        // GET: /Proveedor/Details/5

        public ActionResult Details(long id = 0)
        {
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        //
        // GET: /Proveedor/Create

        public ActionResult Create()
        {
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre");
            return View();
        }

        //
        // POST: /Proveedor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.proveedor.Add(proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", proveedor.MUNICIPIO_id);
            return View(proveedor);
        }

        //
        // GET: /Proveedor/Edit/5

        public ActionResult Edit(long id = 0)
        {
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", proveedor.MUNICIPIO_id);
            return View(proveedor);
        }

        //
        // POST: /Proveedor/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", proveedor.MUNICIPIO_id);
            return View(proveedor);
        }

        //
        // GET: /Proveedor/Delete/5

        public ActionResult Delete(long id = 0)
        {
            proveedor proveedor = db.proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        //
        // POST: /Proveedor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            proveedor proveedor = db.proveedor.Find(id);
            db.proveedor.Remove(proveedor);
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