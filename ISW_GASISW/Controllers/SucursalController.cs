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
    public class SucursalController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Sucursal/

        public ActionResult Index()
        {
            var sucursal = db.sucursal.Include(s => s.municipio);
            return View(sucursal.ToList());
        }

        //
        // GET: /Sucursal/Details/5

        public ActionResult Details(long id = 0)
        {
            sucursal sucursal = db.sucursal.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        //
        // GET: /Sucursal/Create

        public ActionResult Create()
        {
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre");
            return View();
        }

        //
        // POST: /Sucursal/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.sucursal.Add(sucursal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", sucursal.MUNICIPIO_id);
            return View(sucursal);
        }

        //
        // GET: /Sucursal/Edit/5

        public ActionResult Edit(long id = 0)
        {
            sucursal sucursal = db.sucursal.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", sucursal.MUNICIPIO_id);
            return View(sucursal);
        }

        //
        // POST: /Sucursal/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sucursal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", sucursal.MUNICIPIO_id);
            return View(sucursal);
        }

        //
        // GET: /Sucursal/Delete/5

        public ActionResult Delete(long id = 0)
        {
            sucursal sucursal = db.sucursal.Find(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        //
        // POST: /Sucursal/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            sucursal sucursal = db.sucursal.Find(id);
            db.sucursal.Remove(sucursal);
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