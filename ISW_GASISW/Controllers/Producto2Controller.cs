﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISW_GASISW.Models;

namespace ISW_GASISW.Controllers
{
    public class Producto2Controller : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Producto2/

        public ActionResult Index()
        {
            var producto = db.producto.Include(p => p.categoria_producto).Include(p => p.presentacion_producto).Include(p => p.proveedor);
            return View(producto.ToList());
        }

        //
        // GET: /Producto2/Details/5

        public ActionResult Details(long id = 0)
        {
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        //
        // GET: /Producto2/Create

        public ActionResult Create()
        {
            ViewBag.CATEGORIA_PRODUCTO_id = new SelectList(db.categoria_producto, "id", "nombre");
            ViewBag.PRESENTACION_PRODUCTO_id = new SelectList(db.presentacion_producto, "id", "nombre");
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre");
            return View();
        }

        //
        // POST: /Producto2/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(producto producto)
        {
            if (ModelState.IsValid)
            {
                db.producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CATEGORIA_PRODUCTO_id = new SelectList(db.categoria_producto, "id", "nombre", producto.CATEGORIA_PRODUCTO_id);
            ViewBag.PRESENTACION_PRODUCTO_id = new SelectList(db.presentacion_producto, "id", "nombre", producto.PRESENTACION_PRODUCTO_id);
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre", producto.PROVEEDOR_id);
            return View(producto);
        }

        //
        // GET: /Producto2/Edit/5

        public ActionResult Edit(long id = 0)
        {
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORIA_PRODUCTO_id = new SelectList(db.categoria_producto, "id", "nombre", producto.CATEGORIA_PRODUCTO_id);
            ViewBag.PRESENTACION_PRODUCTO_id = new SelectList(db.presentacion_producto, "id", "nombre", producto.PRESENTACION_PRODUCTO_id);
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre", producto.PROVEEDOR_id);
            return View(producto);
        }

        //
        // POST: /Producto2/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORIA_PRODUCTO_id = new SelectList(db.categoria_producto, "id", "nombre", producto.CATEGORIA_PRODUCTO_id);
            ViewBag.PRESENTACION_PRODUCTO_id = new SelectList(db.presentacion_producto, "id", "nombre", producto.PRESENTACION_PRODUCTO_id);
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre", producto.PROVEEDOR_id);
            return View(producto);
        }

        //
        // GET: /Producto2/Delete/5

        public ActionResult Delete(long id = 0)
        {
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        //
        // POST: /Producto2/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            producto producto = db.producto.Find(id);
            db.producto.Remove(producto);
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