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
    public class SeccionBodegaController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /SeccionBodega/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "SeccionBodega", "Index");
            if (Validacion)
            {
            return View(db.seccion_bodega.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /SeccionBodega/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "SeccionBodega", "Details");
            if (Validacion)
            {
                seccion_bodega seccion_bodega = db.seccion_bodega.Find(id);
                if (seccion_bodega == null)
                {
                    return HttpNotFound();
                }
                return View(seccion_bodega);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /SeccionBodega/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "SeccionBodega", "Create");
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
        // POST: /SeccionBodega/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(seccion_bodega seccion_bodega)
        {
            if (ModelState.IsValid)
            {
                db.seccion_bodega.Add(seccion_bodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seccion_bodega);
        }

        //
        // GET: /SeccionBodega/Edit/5

        public ActionResult Edit(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "SeccionBodega", "Edit");
            if (Validacion)
            {
                seccion_bodega seccion_bodega = db.seccion_bodega.Find(id);
                if (seccion_bodega == null)
                {
                    return HttpNotFound();
                }
                return View(seccion_bodega);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /SeccionBodega/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(seccion_bodega seccion_bodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seccion_bodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seccion_bodega);
        }

        //
        // GET: /SeccionBodega/Delete/5

        public ActionResult Delete(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "SeccionBodega", "Delete");
            if (Validacion)
            {
                seccion_bodega seccion_bodega = db.seccion_bodega.Find(id);
                if (seccion_bodega == null)
                {
                    return HttpNotFound();
                }
                return View(seccion_bodega);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /SeccionBodega/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            seccion_bodega seccion_bodega = db.seccion_bodega.Find(id);
            db.seccion_bodega.Remove(seccion_bodega);
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