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
    public class D_ORDEN_COMPRAController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /D_ORDEN_COMPRA/

        public ActionResult Index()
        {
            var d_orden_compra = db.d_orden_compra.Include(d => d.m_orden_compra).Include(d => d.producto);
            return View(d_orden_compra.ToList());
        }

        //
        // GET: /D_ORDEN_COMPRA/Details/5

        public ActionResult Details(long id = 0)
        {
            d_orden_compra d_orden_compra = db.d_orden_compra.Find(id);
            if (d_orden_compra == null)
            {
                return HttpNotFound();
            }
            return View(d_orden_compra);
        }

        //
        // GET: /D_ORDEN_COMPRA/Create

        public ActionResult Create()
        {
            ViewBag.M_ORDEN_COMPRA_id = new SelectList(db.m_orden_compra, "id", "id");
            ViewBag.PRODUCTO_id = new SelectList(db.producto, "id", "nombre");
            return View();
        }

        //
        // POST: /D_ORDEN_COMPRA/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(d_orden_compra d_orden_compra)
        {
            if (ModelState.IsValid)
            {
                db.d_orden_compra.Add(d_orden_compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.M_ORDEN_COMPRA_id = new SelectList(db.m_orden_compra, "id", "id", d_orden_compra.M_ORDEN_COMPRA_id);
            ViewBag.PRODUCTO_id = new SelectList(db.producto, "id", "nombre", d_orden_compra.PRODUCTO_id);
            return View(d_orden_compra);
        }

        //
        // GET: /D_ORDEN_COMPRA/Edit/5

        public ActionResult Edit(long id = 0)
        {
            d_orden_compra d_orden_compra = db.d_orden_compra.Find(id);
            if (d_orden_compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.M_ORDEN_COMPRA_id = new SelectList(db.m_orden_compra, "id", "id", d_orden_compra.M_ORDEN_COMPRA_id);
            ViewBag.PRODUCTO_id = new SelectList(db.producto, "id", "nombre", d_orden_compra.PRODUCTO_id);
            return View(d_orden_compra);
        }

        //
        // POST: /D_ORDEN_COMPRA/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(d_orden_compra d_orden_compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d_orden_compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.M_ORDEN_COMPRA_id = new SelectList(db.m_orden_compra, "id", "id", d_orden_compra.M_ORDEN_COMPRA_id);
            ViewBag.PRODUCTO_id = new SelectList(db.producto, "id", "nombre", d_orden_compra.PRODUCTO_id);
            return View(d_orden_compra);
        }

        //
        // GET: /D_ORDEN_COMPRA/Delete/5

        public ActionResult Delete(long id = 0)
        {
            d_orden_compra d_orden_compra = db.d_orden_compra.Find(id);
            if (d_orden_compra == null)
            {
                return HttpNotFound();
            }
            return View(d_orden_compra);
        }

        //
        // POST: /D_ORDEN_COMPRA/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            d_orden_compra d_orden_compra = db.d_orden_compra.Find(id);
            db.d_orden_compra.Remove(d_orden_compra);
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