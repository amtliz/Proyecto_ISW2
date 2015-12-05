using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISW_GASISW.Models;

using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;


namespace ISW_GASISW.Controllers
{
    public class OrdenCompraController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /OrdenCompra/
        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "OrdenCompra", "Index");
            if (Validacion)
            {
                Session["M_O_C"] = null;
                ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre"); 
                var Lista = db.d_orden_compra.Select(p => p.m_orden_compra.id).Take(1);
                List<m_orden_compra> Lista2 = db.m_orden_compra.Include(p => p.d_orden_compra).Where(p => Lista.Contains(p.id)).ToList();
                M_M_Orden_Compra MMOC = new M_M_Orden_Compra();
                MMOC.LMOC = Lista2;            
                return View(MMOC);
            }
            else
            {
                return RedirectToAction("Error");
            }
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
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "OrdenCompra", "Create");
            if (Validacion)
            {
                int Maestro = Convert.ToInt16(Session["M_O_C"]);
                string cantidadDefault = "0";
                int proveedor = Convert.ToInt16(db.m_orden_compra.Where(x => x.id == Maestro).Select(X => X.PROVEEDOR_id).Single());
                List<d_orden_compra> LDOC = new List<d_orden_compra>();
                List<producto> Producto = db.producto.Include(p => p.categoria_producto).Include(p => p.presentacion_producto).Where(x => x.PROVEEDOR_id == proveedor).ToList();
                foreach (var prod in Producto)
                {
                    d_orden_compra DOC = new d_orden_compra();
                    DOC.M_ORDEN_COMPRA_id = Maestro;
                    DOC.producto = prod;
                    DOC.cantidad = cantidadDefault;

                    LDOC.Add(DOC);
                }
                M_D_Orden_Compra MDOC = new M_D_Orden_Compra();
                MDOC.Lista = LDOC;

                return View(MDOC);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /OrdenCompra/Create
        [HttpPost]
        public ActionResult Create(M_D_Orden_Compra MDOC)
        {
            //Obteniendo el valor del Master
            int Maestro = Convert.ToInt16(Session["M_O_C"]);
            //recorriendo Arrays
            for (int i = 0; i < MDOC.Cant.Count(); i++)
            {
                //Obtener Valores de la clase
                string cantidad = MDOC.Cant[i];
                string producto = MDOC.id[i];
                if (cantidad != "")
                {
                    d_orden_compra DOC = new d_orden_compra();
                    DOC.M_ORDEN_COMPRA_id = Maestro;
                    DOC.PRODUCTO_id = Convert.ToInt16(producto);
                    DOC.cantidad = cantidad;

                    db.d_orden_compra.Add(DOC);
                    db.SaveChanges();
                }
            }

            Session["M_O_C"] = null;
            return RedirectToAction("Index");
        }

        //
        // GET: /OrdenCompra/Aprobar
        public ActionResult Aprobar(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "OrdenCompra", "Aprobar");
            if (Validacion)
            {
                m_orden_compra MOC = db.m_orden_compra.Where(p => p.id == id).Select(p => p).Single();
                MOC.estado = true;
                MOC.aprobado_EMPLEADO_id = Convert.ToInt16(Session["Empleado_id"]);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /OrdenCompra/Denegar
        public ActionResult Denegar(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "OrdenCompra", "Denegar");
            if (Validacion)
            {
                m_orden_compra MOC = db.m_orden_compra.Where(p => p.id == id).Select(p => p).Single();
                MOC.estado = false;
                MOC.aprobado_EMPLEADO_id = Convert.ToInt16(Session["Empleado_id"]);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
                return RedirectToAction("Index");
            }
            {
                return RedirectToAction("Error");
            }
        }

        //
        //GET: /OrdenCompra/Reporte
        public ActionResult Proveedores()
        {
            List<proveedor> L = db.proveedor.ToList();
            return View(L);
        }

        //GET: /OrdenCompra/Ordenes
        public ActionResult Ordenes(long id=0)
        {
            Session["Proveedor"] = null;
            var Lista = db.d_orden_compra.Select(p => p.m_orden_compra.id).Take(1);
            List<m_orden_compra> Lista2 = db.m_orden_compra.Include(p => p.d_orden_compra).Where(p => Lista.Contains(p.id) && p.proveedor.id == id).ToList();
            M_M_Orden_Compra MMOC = new M_M_Orden_Compra();
            MMOC.LMOC = Lista2;
            Session["Proveedor"] = id;
            return View(MMOC);
        }

        public ActionResult ReporteOrdenes()
        {
            long id = Convert.ToInt16(Session["Proveedor"]);

            LocalReport LR = new LocalReport();

            string deviceInfo =
            "<DeviceInfo>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            LR.ReportPath = Server.MapPath("~/Reportes/Report1.rdlc");
            var ListaX = db.d_orden_compra.Select(p => p.m_orden_compra.id).Take(1);
            //List<m_orden_compra> Lista = new List<m_orden_compra>();

            var Lista = db.m_orden_compra.Where(p => ListaX.Contains(p.id) && p.proveedor.id == id).Select(p => new { p.id,p.fecha_emitida,p.empleado.nombre1,p.empleado.apellido1,p.estado,aproNom = p.empleado1.nombre1,aproApe = p.empleado1.apellido1,p.proveedor.nombre }).ToList();
            ReportDataSource RD = new ReportDataSource("DataSet1", Lista);
            LR.DataSources.Add(RD);

            byte[] bytes = LR.Render("PDF", deviceInfo);

            return File(bytes, "PDF");
        }
    }
}
