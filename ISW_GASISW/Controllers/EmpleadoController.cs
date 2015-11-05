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

        //
        // GET: /Empleado/

        public ActionResult Index()
        {
            var empleado = db.empleado.Include(e => e.municipio).Include(e => e.sucursal);
            return View(empleado.ToList());
        }

        //
        // GET: /Empleado/Details/5

        public ActionResult Details(long id = 0)
        {
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        //
        // GET: /Empleado/Create

        public ActionResult Create()
        {
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre");
            ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre");
            return View();
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
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", empleado.MUNICIPIO_id);
            ViewBag.SUCURSAL_id = new SelectList(db.sucursal, "id", "nombre", empleado.SUCURSAL_id);
            return View(empleado);
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
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
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