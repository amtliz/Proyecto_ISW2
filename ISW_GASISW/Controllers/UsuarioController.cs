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
    public class UsuarioController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            var usuario = db.usuario.Include(u => u.empleado).Include(u => u.rol);
            return View(usuario.ToList());
        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(long id = 0)
        {
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            ViewBag.EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1");
            ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre");
            return View();
        }

        //
        // POST: /Usuario/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", usuario.EMPLEADO_id);
            ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre", usuario.ROL_id);
            return View(usuario);
        }

        //
        // GET: /Usuario/Edit/5

        public ActionResult Edit(long id = 0)
        {
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", usuario.EMPLEADO_id);
            ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre", usuario.ROL_id);
            return View(usuario);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1", usuario.EMPLEADO_id);
            ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre", usuario.ROL_id);
            return View(usuario);
        }

        //
        // GET: /Usuario/Delete/5

        public ActionResult Delete(long id = 0)
        {
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            usuario usuario = db.usuario.Find(id);
            db.usuario.Remove(usuario);
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