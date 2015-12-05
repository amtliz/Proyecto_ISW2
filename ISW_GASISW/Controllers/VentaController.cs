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
    public class VentaController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();
        C_Ingreso_Caja CIC = new C_Ingreso_Caja();

        //
        // GET: /Venta/
        public ActionResult Index()
        {
            //int rol = Convert.ToInt16(Session["Rol_id"]);
            //bool Validacion = SEG.ValidarAcceso(rol, "Venta", "Index");
            //if (Validacion)
            //{
                Session["M_V"] = null;
                var Lista = db.d_venta.Select(p => p.m_venta.id).Take(1);
                List<m_venta> Lista2 = db.m_venta.Include(p => p.d_venta).Where(p => Lista.Contains(p.id)).ToList();
                M_M_Venta MMV = new M_M_Venta();
                MMV.ListaMV = Lista2;
                return View(MMV);
            //}
            //else
            //{
            //    return RedirectToAction("Error");
            //}
        }

        //
        // GET: /Venta/Create
        public ActionResult Create()
        {
            //int rol = Convert.ToInt16(Session["Rol_id"]);
            //bool Validacion = SEG.ValidarAcceso(rol, "Venta", "Create");
            //if (Validacion)
            //{
                Session["M_V"] = null;

                ViewBag.TipoFacturacion = db.tipo_facturacion.ToList();
                ViewBag.Producto = db.producto.ToList();

                m_venta MV = new m_venta();
                MV.fecha_venta = DateTime.Today;
                MV.EMPLEADO_id = Convert.ToInt16(Session["Empleado_id"]);
                MV.total = 0;

                db.m_venta.Add(MV);
                db.SaveChanges();

                int MASTER = Convert.ToInt16(db.m_venta.Max(x => x.id));
                Session["M_V"] = MASTER;

                return View();
            //}
            //else
            //{
            //    return RedirectToAction("Error");
            //}
        }

        //
        // POST: /Compra/Create
        [HttpPost]
        public ActionResult Create(M_D_Venta MDV)
        {
            //Obteniendo el valor del Master
            int Maestro = Convert.ToInt16(Session["M_V"]);
            int TipoFacturacion = MDV.TipoFacturacion;
            float totalMaster = 0;
            int caja = Convert.ToInt16(Session["Caja_id"]);
            //recorriendo Arrays
            for (int i = 0; i < MDV.Cant.Count(); i++)
            {
                //Obtener Valores de la clase
                string cantidad = MDV.Cant[i];
                string producto = MDV.id[i];
                int producto_id = int.Parse(producto);
                float precio = db.producto.Where(x => x.id == producto_id).Select(x => x.precio_venta_u).Single();
                float total = int.Parse(cantidad) * precio;
                if (cantidad != "")
                {
                    d_venta DV = new d_venta();
                    DV.PRODUCTO_id = int.Parse(producto);
                    DV.cantidad_producto = int.Parse(cantidad);
                    DV.total = total;
                    DV.M_VENTA_id = Maestro;
                    DV.precio_u = precio;

                    db.d_venta.Add(DV);
                    db.SaveChanges();

                    totalMaster = totalMaster + DV.total;
                }
            }

            m_venta MV = db.m_venta.Where(p => p.id == Maestro).Select(p => p).Single();
            MV.total = totalMaster;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            if (TipoFacturacion == 1)           //Nota de Credito Venta
            {
                return RedirectToAction("Create", "NotaCreditoVenta");
            }
            else if(TipoFacturacion == 2)       //tiquete
            {
                facturacion F = new facturacion();
                F.TIPO_FACTURACION_id = TipoFacturacion;
                F.M_VENTA_id = MV.id;
                db.facturacion.Add(F);
                db.SaveChanges();

                CIC.Ingreso(caja, totalMaster, Maestro);
                return Redirect("Index");

            }
            else if (TipoFacturacion == 3)       //Consumidor
            {
                CIC.Ingreso(caja, totalMaster, Maestro);
                return RedirectToAction("ConsumidorFinal");
            }
            else if (TipoFacturacion == 4)       //Credito fiscal
            {
                CIC.Ingreso(caja, totalMaster, Maestro);
                return RedirectToAction("CreditoFiscal");
            }
            else
            {
                return RedirectToAction("Index");
            } 

        }
        //GET

        public ActionResult ConsumidorFinal()
        {
            return Redirect("Index");
        }

        public ActionResult CreditoFiscal()
        {
            return Redirect("Index");
        }
    }
}
