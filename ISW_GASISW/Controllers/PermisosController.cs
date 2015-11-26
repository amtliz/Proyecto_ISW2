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
    public class PermisosController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /Permisos/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Permisos", "Index");
            if (Validacion)
            {
                return View(db.permisos.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Permisos/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Permisos", "Details");
            if (Validacion)
            {
                permisos permisos = db.permisos.Find(id);
                if (permisos == null)
                {
                    return HttpNotFound();
                }
                return View(permisos);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Permisos/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Permisos", "Create");
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
        // POST: /Permisos/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.permisos.Add(permisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(permisos);
        }

        //
        // GET: /Permisos/Edit/5

        public ActionResult Edit(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Permisos", "Edit");
            if (Validacion)
            {
                permisos permisos = db.permisos.Find(id);
                if (permisos == null)
                {
                    return HttpNotFound();
                }
                return View(permisos);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Permisos/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(permisos);
        }

        //
        // GET: /Permisos/Delete/5

        public ActionResult Delete(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Permisos", "Delete");
            if (Validacion)
            {
                permisos permisos = db.permisos.Find(id);
                if (permisos == null)
                {
                    return HttpNotFound();
                }
                return View(permisos);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Permisos/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            permisos permisos = db.permisos.Find(id);
            db.permisos.Remove(permisos);
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