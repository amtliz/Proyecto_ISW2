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
    public class FacturacionController : Controller
    {

        private gasiswEntities db = new gasiswEntities();

        Seguridad SEG = new Seguridad();
        //
        // GET: /Facturacion/

        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Facturacion", "Index");
            if (Validacion)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /Facturacion/Create
        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "Facturacion", "Create");
            if (Validacion)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        public ActionResult FacturaDetalle()
        {       
            var Lista = db.d_venta.Select(p => p.m_venta.id).Take(1);
            List<facturacion> Lista2 = db.facturacion.Include(p => p.m_venta.d_venta).Where(p => Lista.Contains(p.M_VENTA_id)).ToList();
            M_M_Factura MMC = new M_M_Factura();
            MMC.LMC = Lista2;
            return View(MMC); 
        }


        public ActionResult ReporteDetalleFactura(long id)
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

            LR.ReportPath = Server.MapPath("~/Reportes/ReportDetalleFactura.rdlc");

            var query = from ft in db.facturacion
                        join dt in db.d_venta
                        on ft.M_VENTA_id equals dt.M_VENTA_id
                        where ft.id == id
                        select new
                        {
                            id = ft.id,
                            venta = ft.M_VENTA_id,
                            fecha_venta = ft.m_venta.fecha_venta,
                            nombre1 = ft.cliente.nombre1,
                            apellido1 = ft.cliente.apellido1,
                            num_documento = ft.num_documento,
                            facturacion = ft.tipo_facturacion.nombre,
                            documento = ft.tipo_documento.nombre,
                            PRODUCTO_id = dt.PRODUCTO_id,
                            cantidad_producto = dt.cantidad_producto,
                            total = dt.total,
                            precio_u = dt.precio_u,
                            producto = dt.producto.nombre,
                            fac_total = ft.m_venta.total 
                        };

            ReportDataSource RD = new ReportDataSource("DsDetFact", query.ToList());
            LR.DataSources.Add(RD);

            byte[] bytes = LR.Render("PDF", deviceInfo);

            return File(bytes, "PDF");
        }


        


    }
}
