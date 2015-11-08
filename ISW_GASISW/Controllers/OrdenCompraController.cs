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
    public class OrdenCompraController : Controller
    {
        private gasiswEntities db = new gasiswEntities();

        //
        // GET: /OrdenCompra/

        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre");
            var lstProducto = new List<producto>(db.producto);
            return View(lstProducto);
        }

    }
}
