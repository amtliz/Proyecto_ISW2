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
    public class CompraController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();
        C_Ajuste_Invetario CAI = new C_Ajuste_Invetario();

        //
        // GET: /Compra/
        public ActionResult Index()
        {
            //int rol = Convert.ToInt16(Session["Rol_id"]);
            //bool Validacion = SEG.ValidarAcceso(rol, "Compra", "Index");
            //if (Validacion)
            //{
                Session["M_C"] = null;
                ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre");
                var Lista = db.d_compra.Select(p => p.m_compra.id).Take(1);
                List<m_compra> Lista2 = db.m_compra.Include(p => p.d_compra).Where(p => Lista.Contains(p.id)).ToList();
                M_M_Compra MMC = new M_M_Compra();
                MMC.LMC = Lista2;
                return View(MMC);
            // }
            //else
            //{
            //    return RedirectToAction("Error");
            //}
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
            //int rol = Convert.ToInt16(Session["Rol_id"]);
            //bool Validacion = SEG.ValidarAcceso(rol, "Compra", "Create");
            //if (Validacion)
            //{
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
            //}
            //else
            //{
            //    return RedirectToAction("Error");
            //}
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

                    int p = Convert.ToInt16(producto);   
                    //Guardo Lote
                    CAI.guardarLote(p, 1);

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


        public ActionResult ReporteCompras()
        {
            long id = Convert.ToInt16(Session["Proveedor"]);
            //DateTime fecha = Convert.ToDateTime(Session["FechaCompra"]);

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

            LR.ReportPath = Server.MapPath("~/Reportes/ReportCompras.rdlc");
            var ListaX = db.d_compra.Select(p => p.m_compra.id).Take(1);
            

            var Lista = db.m_compra.Where(p => ListaX.Contains(p.id) && p.proveedor.id == id ).Select(p => new { p.id, p.fecha_compra, p.empleado.nombre1, p.empleado.apellido1, p.total_compra, p.proveedor.nombre , p.tipo_compra}).ToList();
            ReportDataSource RD = new ReportDataSource("DsCompra", Lista);
            LR.DataSources.Add(RD);

            byte[] bytes = LR.Render("PDF", deviceInfo);

            return File(bytes, "PDF");
        }

        public ActionResult DatosReporteCompras()
       {
           ViewBag.PROVEEDOR_id = new SelectList(db.proveedor, "id", "nombre");
           return View();
       }

        [HttpPost]
        public ActionResult DatosReporteCompras(FormCollection form)
        {
            Session["Proveedor"] = form["proveedor"];
            return RedirectToAction("ReporteCompras");
        }

        public ActionResult ReporteDetalleCompra(long id)
        {
                     
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

            LR.ReportPath = Server.MapPath("~/Reportes/ReportDetalleCompra.rdlc");

            var query = from cm in db.m_compra
                        join dt in db.d_compra
                        on cm.id equals dt.M_COMPRA_id
                        where cm.id == id
                        select new
                        {
                            id=cm.id,
                            fecha_compra= cm.fecha_compra, 
                            nombre1= cm.empleado.nombre1, 
                            apellido1= cm.empleado.apellido1, 
                            total_compra = cm.total_compra,
                            nombre = cm.proveedor.nombre,
                            tipo_compra = cm.tipo_compra.nombre,
                            PRODUCTO_id = dt.PRODUCTO_id,
                            cantidad_producto = dt.cantidad_producto,
                            costo_unitario = dt.costo_unitario,
                            total=dt.total,
                            producto = dt.producto.nombre
                        };
                  
            ReportDataSource RD = new ReportDataSource("DsDetCompra", query.ToList());
            LR.DataSources.Add(RD);

            byte[] bytes = LR.Render("PDF", deviceInfo);

            return File(bytes, "PDF");
        }

        public ActionResult CompraDetalle()
        {
            var Lista = db.d_compra.Select(p => p.m_compra.id).Take(1);
            List<m_compra> Lista2 = db.m_compra.Include(p => p.d_compra).Where(p => Lista.Contains(p.id)).ToList();
            M_M_Compra MMC = new M_M_Compra();
            MMC.LMC = Lista2;
            return View(MMC); 
        }
        
    }
}
