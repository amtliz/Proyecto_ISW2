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
    public class TipoFacturacionController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /TipoFacturacion/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoFacturacion", "Index");
            if (Validacion)
            {
                return View(db.tipo_facturacion.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /TipoFacturacion/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoFacturacion", "Details");
            if (Validacion)
            {
                tipo_facturacion tipo_facturacion = db.tipo_facturacion.Find(id);
                if (tipo_facturacion == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_facturacion);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /TipoFacturacion/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoFacturacion", "Create");
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
        // POST: /TipoFacturacion/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tipo_facturacion tipo_facturacion)
        {
            if (ModelState.IsValid)
            {
                db.tipo_facturacion.Add(tipo_facturacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_facturacion);
        }

        //
        // GET: /TipoFacturacion/Edit/5

        public ActionResult Edit(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoFacturacion", "Edit");
            if (Validacion)
            {
                tipo_facturacion tipo_facturacion = db.tipo_facturacion.Find(id);
                if (tipo_facturacion == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_facturacion);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /TipoFacturacion/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tipo_facturacion tipo_facturacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_facturacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_facturacion);
        }

        //
        // GET: /TipoFacturacion/Delete/5

        public ActionResult Delete(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoFacturacion", "Delete");
            if (Validacion)
            {
                tipo_facturacion tipo_facturacion = db.tipo_facturacion.Find(id);
                if (tipo_facturacion == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_facturacion);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /TipoFacturacion/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tipo_facturacion tipo_facturacion = db.tipo_facturacion.Find(id);
            db.tipo_facturacion.Remove(tipo_facturacion);
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