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
        Seguridad SEG = new Seguridad();

        //
        // GET: /Caja/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Caja", "Index");
            if (Validacion)
            {
                var caja = db.caja.Include(c => c.sucursal);
                return View(caja.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        //
        // GET: /Caja/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Caja", "Details");
            if (Validacion)
            {
                caja caja = db.caja.Find(id);
                if (caja == null)
                {
                    return HttpNotFound();
                }
                return View(caja);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Caja/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Caja", "Create");
            if (Validacion)
            {
                ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre");
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
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
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Caja", "Edit");
            if (Validacion)
            {
                caja caja = db.caja.Find(id);
                if (caja == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre", caja.SUCURSAL_id);
                return View(caja);
            }
            else
            {
                return RedirectToAction("Error");
            }
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
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Caja", "Delete");
            if (Validacion)
            {
                caja caja = db.caja.Find(id);
                if (caja == null)
                {
                    return HttpNotFound();
                }
                return View(caja);
            }
            else
            {
                return RedirectToAction("Error");
            }
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