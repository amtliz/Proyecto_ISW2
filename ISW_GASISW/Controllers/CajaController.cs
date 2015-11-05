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
    public class CajaController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Caja/

        public ActionResult Index()
        {
            var caja = db.caja.Include(c => c.sucursal);
            return View(caja.ToList());
        }

        //
        // GET: /Caja/Details/5

        public ActionResult Details(long id = 0)
        {
            caja caja = db.caja.Find(id);
            if (caja == null)
            {
                return HttpNotFound();
            }
            return View(caja);
        }

        //
        // GET: /Caja/Create

        public ActionResult Create()
        {
            ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre");
            return View();
        }

        //
        // POST: /Caja/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(caja caja)
        {
            if (ModelState.IsValid)
            {
                db.caja.Add(caja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre", caja.SUCURSAL_id);
            return View(caja);
        }

        //
        // GET: /Caja/Edit/5

        public ActionResult Edit(long id = 0)
        {
            caja caja = db.caja.Find(id);
            if (caja == null)
            {
                return HttpNotFound();
            }
            ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre", caja.SUCURSAL_id);
            return View(caja);
        }

        //
        // POST: /Caja/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(caja caja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre", caja.SUCURSAL_id);
            return View(caja);
        }

        //
        // GET: /Caja/Delete/5

        public ActionResult Delete(long id = 0)
        {
            caja caja = db.caja.Find(id);
            if (caja == null)
            {
                return HttpNotFound();
            }
            return View(caja);
        }

        //
        // POST: /Caja/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            caja caja = db.caja.Find(id);
            db.caja.Remove(caja);
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