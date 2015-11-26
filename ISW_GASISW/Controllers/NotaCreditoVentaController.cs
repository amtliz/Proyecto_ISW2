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
    public class NotaCreditoVentaController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /NotaCreditoVenta/
        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "NotaCreditoVenta", "Index");
            if (Validacion)
            {
                List<nota_credito_venta> LNCV = db.nota_credito_venta.ToList();
                return View(LNCV);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /NotaCreditoVenta/Create
        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "NotaCreditoVenta", "Create");
            if (Validacion)
            {
                //Obteniendo el valor del Master Compra
                int Maestro = Convert.ToInt16(Session["M_V"]);
                ViewBag.Cliente_id = db.cliente.ToList();
                m_venta MV = db.m_venta.Where(p => p.id == Maestro).Single();
                M_Nota_Credito_Venta MNCV = new M_Nota_Credito_Venta();
                MNCV.MV = MV;
                return View(MNCV);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /NotaCreditoVenta/Create
        [HttpPost]
        public ActionResult Create(M_Nota_Credito_Venta MNCV)
        {
            //Obteniendo el valor del Master
            int Maestro = Convert.ToInt16(Session["M_V"]);
            m_venta MV = db.m_venta.Where(p => p.id == Maestro).Single();
            nota_credito_venta NCV = new nota_credito_venta();
            NCV.CLIENTE_id = MNCV.cliente;
            NCV.plazo = Convert.ToDateTime(MNCV.plazo);
            NCV.fecha_extendido = MV.fecha_venta;
            NCV.M_VENTA_id = MV.id;
            NCV.estado = true;

            db.nota_credito_venta.Add(NCV);
            db.SaveChanges();

            Session["M_V"] = null;
            return RedirectToAction("Index");
        }

        //
        // GET: /NotaCreditoVenta/Pagar
        public ActionResult Pagar(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "NotaCreditoVenta", "Pagar");
            if (Validacion)
            {
                nota_credito_venta NCV = db.nota_credito_venta.Where(p => p.id == id).Select(p => p).Single();
                NCV.fecha_pagado = DateTime.Today;
                facturacion FAC = new facturacion();
                FAC.TIPO_FACTURACION_id = 2;
                FAC.M_VENTA_id = Convert.ToInt16(NCV.id);
                db.facturacion.Add(FAC);
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
    }
}
