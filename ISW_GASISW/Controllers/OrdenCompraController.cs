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
            Session["M_O_C"] = null;
            ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre");
            return View();
        }

        //
        // POST: /OrdenCompra/
        [HttpPost]
        public ActionResult Index(M_M_Orden_Compra MMOC)
        {
            m_orden_compra MOP = new m_orden_compra();
            MOP.fecha_emitida = DateTime.Today;
            MOP.emitido_EMPLEADO_id = Convert.ToInt16(Session["Empleado_id"]);
            MOP.estado = false;
            MOP.PROVEEDOR_id = MMOC.proveedor;

            db.m_orden_compra.Add(MOP);
            db.SaveChanges();

            int MASTER = Convert.ToInt16(db.m_orden_compra.Max(x => x.id));
            Session["M_O_C"] = MASTER;

            return RedirectToAction("Create");
        }

        //
        // GET: /OrdenCompra/Create
        public ActionResult Create()
        {
            int Maestro = Convert.ToInt16(Session["M_O_C"]);
            int proveedor = Convert.ToInt16(db.m_orden_compra.Where(x => x.id == Maestro).Select(X => X.PROVEEDOR_id).Single());
            List<producto> Producto = db.producto.Include(p => p.categoria_producto).Include(p => p.presentacion_producto).Where(x => x.PROVEEDOR_id == proveedor).ToList();
            ViewBag.PRODUCTO = Producto;
            return View();
        }

    }
}
