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
    public class DepartamentoController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /Departamento/

        public ActionResult Index()
        {
            return View(db.departamento.ToList());
        }

        //
        // GET: /Departamento/Details/5

        public ActionResult Details(long id = 0)
        {
            departamento departamento = db.departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        //
        // GET: /Departamento/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Departamento/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.departamento.Add(departamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departamento);
        }

        //
        // GET: /Departamento/Edit/5

        public ActionResult Edit(long id = 0)
        {
            departamento departamento = db.departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        //
        // POST: /Departamento/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        //
        // GET: /Departamento/Delete/5

        public ActionResult Delete(long id = 0)
        {
            departamento departamento = db.departamento.Find(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        //
        // POST: /Departamento/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            departamento departamento = db.departamento.Find(id);
            db.departamento.Remove(departamento);
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