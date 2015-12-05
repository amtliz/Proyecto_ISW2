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
    public class RolController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /Rol/

        public ActionResult Index()
        {
            //int rol = Convert.ToInt16(Session["Rol_id"]);
            //bool Validacion = SEG.ValidarAcceso(rol, "Rol", "Index");
            //if (Validacion)
            //{
                return View(db.rol.ToList());
            //}
            //else
            //{
            //    return RedirectToAction("Error");
            //}
        }

        //
        // GET: /Rol/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol1 = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol1, "Rol", "Details");
            if (Validacion)
            {
                rol rol = db.rol.Find(id);
                if (rol == null)
                {
                    return HttpNotFound();
                }
                return View(rol);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Rol/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Rol", "Create");
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
        // POST: /Rol/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(rol rol)
        {
            if (ModelState.IsValid)
            {
                db.rol.Add(rol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rol);
        }

        //
        // GET: /Rol/Edit/5

        public ActionResult Edit(long id = 0)
        {
            int rol1 = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol1, "Rol", "Edit");
            if (Validacion)
            {
                rol rol = db.rol.Find(id);
                if (rol == null)
                {
                    return HttpNotFound();
                }
                return View(rol);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Rol/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(rol rol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rol);
        }

        //
        // GET: /Rol/Delete/5

        public ActionResult Delete(long id = 0)
        {
            int rol1 = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol1, "Rol", "Delete");
            if (Validacion)
            {
                rol rol = db.rol.Find(id);
                if (rol == null)
                {
                    return HttpNotFound();
                }
                return View(rol);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Rol/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            rol rol = db.rol.Find(id);
            db.rol.Remove(rol);
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