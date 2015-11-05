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
    public class PresentacionProductoController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /PresentacionProducto/

        public ActionResult Index()
        {
            return View(db.presentacion_producto.ToList());
        }

        //
        // GET: /PresentacionProducto/Details/5

        public ActionResult Details(long id = 0)
        {
            presentacion_producto presentacion_producto = db.presentacion_producto.Find(id);
            if (presentacion_producto == null)
            {
                return HttpNotFound();
            }
            return View(presentacion_producto);
        }

        //
        // GET: /PresentacionProducto/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PresentacionProducto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(presentacion_producto presentacion_producto)
        {
            if (ModelState.IsValid)
            {
                db.presentacion_producto.Add(presentacion_producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(presentacion_producto);
        }

        //
        // GET: /PresentacionProducto/Edit/5

        public ActionResult Edit(long id = 0)
        {
            presentacion_producto presentacion_producto = db.presentacion_producto.Find(id);
            if (presentacion_producto == null)
            {
                return HttpNotFound();
            }
            return View(presentacion_producto);
        }

        //
        // POST: /PresentacionProducto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(presentacion_producto presentacion_producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presentacion_producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(presentacion_producto);
        }

        //
        // GET: /PresentacionProducto/Delete/5

        public ActionResult Delete(long id = 0)
        {
            presentacion_producto presentacion_producto = db.presentacion_producto.Find(id);
            if (presentacion_producto == null)
            {
                return HttpNotFound();
            }
            return View(presentacion_producto);
        }

        //
        // POST: /PresentacionProducto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            presentacion_producto presentacion_producto = db.presentacion_producto.Find(id);
            db.presentacion_producto.Remove(presentacion_producto);
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