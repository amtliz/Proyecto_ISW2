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
    public class CompraController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /Compra/
        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Compra", "Index");
            if (Validacion)
            {
                Session["M_C"] = null;
                ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre");
                var Lista = db.d_compra.Select(p => p.m_compra.id).Take(1);
                List<m_compra> Lista2 = db.m_compra.Include(p => p.d_compra).Where(p => Lista.Contains(p.id)).ToList();
                M_M_Compra MMC = new M_M_Compra();
                MMC.LMC = Lista2;
                return View(MMC);
             }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Compra/
        [HttpPost]
        public ActionResult Index(M_M_Compra MMC)
        {
            m_compra MC = new m_compra();
            MC.fecha_compra = DateTime.Today;
            MC.PROVEEDOR_id = MMC.proveedor;
            MC.total_compra = 0;
            MC.EMPLEADO_id = Convert.ToInt16(Session["Empleado_id"]);
            MC.TIPO_COMPRA_id = 1;

            db.m_compra.Add(MC);
            db.SaveChanges();

            int MASTER = Convert.ToInt16(db.m_compra.Max(x => x.id));
            Session["M_C"] = MASTER;

            return RedirectToAction("Create");
        }

        //
        // GET: /Compra/Create
        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Compra", "Create");
            if (Validacion)
            {
                ViewBag.TipoCompra = db.tipo_compra.ToList();
                int Maestro = Convert.ToInt16(Session["M_C"]);
                int cantidadDefault = 0;
                int costoDefault = 0;
                int totalDefault = 0;
                int proveedor = Convert.ToInt16(db.m_compra.Where(x => x.id == Maestro).Select(X => X.PROVEEDOR_id).Single());
                List<d_compra> LDC = new List<d_compra>();
                List<producto> Producto = db.producto.Include(p => p.categoria_producto).Include(p => p.presentacion_producto).Where(x => x.PROVEEDOR_id == proveedor).ToList();
                foreach (var prod in Producto)
                {
                    d_compra DC = new d_compra();
                    DC.M_COMPRA_id = Maestro;
                    DC.producto = prod;
                    DC.cantidad_producto = cantidadDefault;
                    DC.costo_unitario = costoDefault;
                    DC.total = totalDefault;
                    LDC.Add(DC);
                }
                M_D_Compra MDC = new M_D_Compra();
                MDC.Lista = LDC;
                return View(MDC);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /Compra/Create
        [HttpPost]
        public ActionResult Create(M_D_Compra MDC)
        {
            //Obteniendo el valor del Master
            int Maestro = Convert.ToInt16(Session["M_C"]);
            int TIPOCOMPRA = MDC.TipoCompra;
            float totalMaster = 0;
            //recorriendo Arrays
            for (int i = 0; i < MDC.Cant.Count(); i++)
            {
                //Obtener Valores de la clase
                string cantidad = MDC.Cant[i];
                string producto = MDC.id[i];
                string costo = MDC.cost[i];
                if (cantidad != "" && costo !="")
                {
                    d_compra DC = new d_compra();
                    DC.M_COMPRA_id = Maestro;
                    DC.PRODUCTO_id = Convert.ToInt16(producto);
                    DC.cantidad_producto = Convert.ToInt16(cantidad);
                    DC.costo_unitario = float.Parse(costo);
                    DC.total = DC.cantidad_producto * DC.costo_unitario;

                    db.d_compra.Add(DC);
                    db.SaveChanges();

                    totalMaster = totalMaster + DC.total;
                }
            }

            m_compra MC = db.m_compra.Where(p => p.id == Maestro).Select(p => p).Single();
            MC.TIPO_COMPRA_id = TIPOCOMPRA;
            MC.total_compra = totalMaster;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }            
            //Si es Nota de Credito
            if (MC.TIPO_COMPRA_id == 2)
            {
                return RedirectToAction("Create", "NotaCreditoVenta");
            }
            else
            {
                Session["M_C"] = null;
                return RedirectToAction("Index");
            }            
        }
    }
}
