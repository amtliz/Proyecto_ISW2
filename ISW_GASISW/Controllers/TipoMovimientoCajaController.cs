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
    public class TipoMovimientoCajaController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /TipoMovimientoCaja/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoMovimientoCaja", "Index");
            if (Validacion)
            {
                return View(db.tipo_movimiento.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /TipoMovimientoCaja/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoMovimientoCaja", "Details");
            if (Validacion)
            {
                tipo_movimiento tipo_movimiento = db.tipo_movimiento.Find(id);
                if (tipo_movimiento == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_movimiento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /TipoMovimientoCaja/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoMovimientoCaja", "Create");
            if (Validacion)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /TipoMovimientoCaja/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tipo_movimiento tipo_movimiento)
        {
            if (ModelState.IsValid)
            {
                db.tipo_movimiento.Add(tipo_movimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_movimiento);
        }

        //
        // GET: /TipoMovimientoCaja/Edit/5

        public ActionResult Edit(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoMovimientoCaja", "Edit");
            if (Validacion)
            {
                tipo_movimiento tipo_movimiento = db.tipo_movimiento.Find(id);
                if (tipo_movimiento == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_movimiento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /TipoMovimientoCaja/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tipo_movimiento tipo_movimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_movimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_movimiento);
        }

        //
        // GET: /TipoMovimientoCaja/Delete/5

        public ActionResult Delete(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoMoimientoCaja", "Delete");
            if (Validacion)
            {
                tipo_movimiento tipo_movimiento = db.tipo_movimiento.Find(id);
                if (tipo_movimiento == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_movimiento);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /TipoMovimientoCaja/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tipo_movimiento tipo_movimiento = db.tipo_movimiento.Find(id);
            db.tipo_movimiento.Remove(tipo_movimiento);
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