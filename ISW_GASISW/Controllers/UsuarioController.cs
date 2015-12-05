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
        Seguridad SEG = new Seguridad();

        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            //int rol = Convert.ToInt16(Session["Rol_id"]);
            //bool Validacion = SEG.ValidarAcceso(rol, "Usuario", "Index");
            //if (Validacion)
            //{
                var usuario = db.usuario.Include(u => u.empleado).Include(u => u.rol);
                return View(usuario.ToList());
            //}
            //else
            //{
            //    return RedirectToAction("Error");
            //}
        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Usuario", "Details");
            if (Validacion)
            {
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Usuario", "Create");
            if (Validacion)
            {
                ViewBag.EMPLEADO_id = new SelectList(db.empleado, "id", "nombre1");
                ViewBag.ROL_id = new SelectList(db.rol, "id", "nombre");
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
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
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Usuario", "Edit");
            if (Validacion)
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
            else
            {
                return RedirectToAction("Error");
            }
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
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Usuario", "Delete");
            if (Validacion)
            {
                usuario usuario = db.usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            else
            {
                return RedirectToAction("Error");
            }
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