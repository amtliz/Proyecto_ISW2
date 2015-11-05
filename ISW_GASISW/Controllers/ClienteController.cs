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
    public class ClienteController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Cliente/

        public ActionResult Index()
        {
            var cliente = db.cliente.Include(c => c.municipio);
            return View(cliente.ToList());
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(long id = 0)
        {
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        //
        // GET: /Cliente/Create

        public ActionResult Create()
        {
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre");
            return View();
        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", cliente.MUNICIPIO_id);
            return View(cliente);
        }

        //
        // GET: /Cliente/Edit/5

        public ActionResult Edit(long id = 0)
        {
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", cliente.MUNICIPIO_id);
            return View(cliente);
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MUNICIPIO_id = new SelectList(db.municipio, "id", "nombre", cliente.MUNICIPIO_id);
            return View(cliente);
        }

        //
        // GET: /Cliente/Delete/5

        public ActionResult Delete(long id = 0)
        {
            cliente cliente = db.cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        //
        // POST: /Cliente/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            cliente cliente = db.cliente.Find(id);
            db.cliente.Remove(cliente);
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