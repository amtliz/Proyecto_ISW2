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
    public class FormulariosController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /Formularios/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Formularios", "Index");
            if (Validacion)
            {
                return View(db.formularios.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Formularios/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Formularios", "Details");
            if (Validacion)
            {
                formularios formularios = db.formularios.Find(id);
                if (formularios == null)
                {
                    return HttpNotFound();
                }
                return View(formularios);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Formularios/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Formularios", "Create");
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
        // POST: /Formularios/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(formularios formularios)
        {
            if (ModelState.IsValid)
            {
                db.formularios.Add(formularios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formularios);
        }

        //
        // GET: /Formularios/Edit/5

        public ActionResult Edit(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Formularios", "Edit");
            if (Validacion)
            {
                formularios formularios = db.formularios.Find(id);
                if (formularios == null)
                {
                    return HttpNotFound();
                }
                return View(formularios);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Formularios/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(formularios formularios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formularios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formularios);
        }

        //
        // GET: /Formularios/Delete/5

        public ActionResult Delete(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Formularios", "Delete");
            if (Validacion)
            {
                formularios formularios = db.formularios.Find(id);
                if (formularios == null)
                {
                    return HttpNotFound();
                }
                return View(formularios);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Formularios/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            formularios formularios = db.formularios.Find(id);
            db.formularios.Remove(formularios);
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