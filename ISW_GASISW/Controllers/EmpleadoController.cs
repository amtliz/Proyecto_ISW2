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
    public class EmpleadoController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /Empleado/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Empleado", "Index");
            if (Validacion)
            {
                var empleado = db.empleado.Include(e => e.municipio).Include(e => e.sucursal);
                return View(empleado.ToList());
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Empleado/Details/5

        public ActionResult Details(long id = 0)
        {
             int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Empleado", "Details");
            if (Validacion)
            {
                empleado empleado = db.empleado.Find(id);
                if (empleado == null)
                {
                    return HttpNotFound();
                }
                return View(empleado);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Empleado/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Empleado", "Create");
            if (Validacion)
            {
                ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre");
                ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre");
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Empleado/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", empleado.MUNICIPIO_id);
            ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre", empleado.SUCURSAL_id);
            return View(empleado);
        }

        //
        // GET: /Empleado/Edit/5

        public ActionResult Edit(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Empleado", "Edit");
            if (Validacion)
            {
                empleado empleado = db.empleado.Find(id);
                if (empleado == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", empleado.MUNICIPIO_id);
                ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre", empleado.SUCURSAL_id);
                return View(empleado);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Empleado/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", empleado.MUNICIPIO_id);
            ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre", empleado.SUCURSAL_id);
            return View(empleado);
        }

        //
        // GET: /Empleado/Delete/5

        public ActionResult Delete(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Empleado", "Delete");
            if (Validacion)
            {
                empleado empleado = db.empleado.Find(id);
                if (empleado == null)
                {
                    return HttpNotFound();
                }
                return View(empleado);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Empleado/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            empleado empleado = db.empleado.Find(id);
            db.empleado.Remove(empleado);
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