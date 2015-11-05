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
    public class CategoriaProductoController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /CategoriaProducto/

        public ActionResult Index()
        {
            return View(db.categoria_producto.ToList());
        }

        //
        // GET: /CategoriaProducto/Details/5

        public ActionResult Details(long id = 0)
        {
            categoria_producto categoria_producto = db.categoria_producto.Find(id);
            if (categoria_producto == null)
            {
                return HttpNotFound();
            }
            return View(categoria_producto);
        }

        //
        // GET: /CategoriaProducto/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CategoriaProducto/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(categoria_producto categoria_producto)
        {
            if (ModelState.IsValid)
            {
                db.categoria_producto.Add(categoria_producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoria_producto);
        }

        //
        // GET: /CategoriaProducto/Edit/5

        public ActionResult Edit(long id = 0)
        {
            categoria_producto categoria_producto = db.categoria_producto.Find(id);
            if (categoria_producto == null)
            {
                return HttpNotFound();
            }
            return View(categoria_producto);
        }

        //
        // POST: /CategoriaProducto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(categoria_producto categoria_producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria_producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoria_producto);
        }

        //
        // GET: /CategoriaProducto/Delete/5

        public ActionResult Delete(long id = 0)
        {
            categoria_producto categoria_producto = db.categoria_producto.Find(id);
            if (categoria_producto == null)
            {
                return HttpNotFound();
            }
            return View(categoria_producto);
        }

        //
        // POST: /CategoriaProducto/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            categoria_producto categoria_producto = db.categoria_producto.Find(id);
            db.categoria_producto.Remove(categoria_producto);
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