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
    public class M_ORDEN_COMPRAController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /M_ORDEN_COMPRA/

        public ActionResult Index()
        {
            var m_orden_compra = db.m_orden_compra.Include(m => m.empleado).Include(m => m.empleado1).Include(m => m.proveedor);
            return View(m_orden_compra.ToList());
        }

        //
        // GET: /M_ORDEN_COMPRA/Details/5

        public ActionResult Details(long id = 0)
        {
            m_orden_compra m_orden_compra = db.m_orden_compra.Find(id);
            if (m_orden_compra == null)
            {
                return HttpNotFound();
            }
            return View(m_orden_compra);
        }

        //
        // GET: /M_ORDEN_COMPRA/Create

        public ActionResult Create()
        {
            ViewBag.emitido_EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1");
            ViewBag.aprobado_EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1");
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre");
            return View();
        }

        //
        // POST: /M_ORDEN_COMPRA/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(m_orden_compra m_orden_compra)
        {
            if (ModelState.IsValid)
            {
                db.m_orden_compra.Add(m_orden_compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.emitido_EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", m_orden_compra.emitido_EMPLEADO_id);
            ViewBag.aprobado_EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", m_orden_compra.aprobado_EMPLEADO_id);
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre", m_orden_compra.PROVEEDOR_id);
            return View(m_orden_compra);
        }

        //
        // GET: /M_ORDEN_COMPRA/Edit/5

        public ActionResult Edit(long id = 0)
        {
            m_orden_compra m_orden_compra = db.m_orden_compra.Find(id);
            if (m_orden_compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.emitido_EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", m_orden_compra.emitido_EMPLEADO_id);
            ViewBag.aprobado_EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", m_orden_compra.aprobado_EMPLEADO_id);
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre", m_orden_compra.PROVEEDOR_id);
            return View(m_orden_compra);
        }

        //
        // POST: /M_ORDEN_COMPRA/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(m_orden_compra m_orden_compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_orden_compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.emitido_EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", m_orden_compra.emitido_EMPLEADO_id);
            ViewBag.aprobado_EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", m_orden_compra.aprobado_EMPLEADO_id);
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre", m_orden_compra.PROVEEDOR_id);
            return View(m_orden_compra);
        }

        //
        // GET: /M_ORDEN_COMPRA/Delete/5

        public ActionResult Delete(long id = 0)
        {
            m_orden_compra m_orden_compra = db.m_orden_compra.Find(id);
            if (m_orden_compra == null)
            {
                return HttpNotFound();
            }
            return View(m_orden_compra);
        }

        //
        // POST: /M_ORDEN_COMPRA/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            m_orden_compra m_orden_compra = db.m_orden_compra.Find(id);
            db.m_orden_compra.Remove(m_orden_compra);
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