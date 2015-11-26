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
    public class TipoCompraController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /TipoCompra/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoCompra", "Index");
            if (Validacion)
            {
                return View(db.tipo_compra.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /TipoCompra/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoCompra", "Details");
            if (Validacion)
            {
                tipo_compra tipo_compra = db.tipo_compra.Find(id);
                if (tipo_compra == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_compra);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /TipoCompra/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoCompra", "Create");
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
        // POST: /TipoCompra/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tipo_compra tipo_compra)
        {
            if (ModelState.IsValid)
            {
                db.tipo_compra.Add(tipo_compra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_compra);
        }

        //
        // GET: /TipoCompra/Edit/5

        public ActionResult Edit(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoCompra", "Edit");
            if (Validacion)
            {
                tipo_compra tipo_compra = db.tipo_compra.Find(id);
                if (tipo_compra == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_compra);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /TipoCompra/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tipo_compra tipo_compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_compra);
        }

        //
        // GET: /TipoCompra/Delete/5

        public ActionResult Delete(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "TipoCompra", "Delete");
            if (Validacion)
            {
                tipo_compra tipo_compra = db.tipo_compra.Find(id);
                if (tipo_compra == null)
                {
                    return HttpNotFound();
                }
                return View(tipo_compra);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /TipoCompra/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tipo_compra tipo_compra = db.tipo_compra.Find(id);
            db.tipo_compra.Remove(tipo_compra);
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