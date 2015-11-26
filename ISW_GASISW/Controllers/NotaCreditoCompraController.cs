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
    public class NotaCreditoCompraController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        Seguridad SEG = new Seguridad();

        //
        // GET: /NotaCreditoCompra/
        public ActionResult Index()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "NotaCreditoCompra", "Index");
            if (Validacion)
            {
                List<nota_credito_compra> LNCC = db.nota_credito_compra.ToList();
                return View(LNCC);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // GET: /NotaCreditoCompra/Create
        public ActionResult Create()
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "NotaCreditoCompra", "Create");
            if (Validacion)
            {
                //Obteniendo el valor del Master Compra
                int Maestro = Convert.ToInt16(Session["M_C"]);
                m_compra MC = db.m_compra.Where(p => p.id == Maestro).Single();            
                M_Nota_Credito_Compra MNCC = new M_Nota_Credito_Compra();
                MNCC.MC = MC;
                return View(MNCC);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        //
        // POST: /NotaCreditoCompra/Create
        [HttpPost]
        public ActionResult Create(M_Nota_Credito_Compra MNCC)
        {
            //Obteniendo el valor del Master
            int Maestro = Convert.ToInt16(Session["M_C"]);
            m_compra MC = db.m_compra.Where(p => p.id == Maestro).Single();
            nota_credito_compra NCC = new nota_credito_compra();
            NCC.plazo = Convert.ToDateTime(MNCC.plazo);
            NCC.fecha_extendido = Convert.ToDateTime(MC.fecha_compra);
            NCC.M_COMPRA_id = MC.id;
            NCC.estado = true;

            db.nota_credito_compra.Add(NCC);
            db.SaveChanges();

            Session["M_C"] = null;
            return RedirectToAction("Index");
        }

        //
        // GET: /NotaCreditoCompra/Pagar
        public ActionResult Pagar(long id = 0)
        {
            int rol = Convert.ToInt16(Session["Rol_id"]);
            bool Validacion = SEG.ValidarAcceso(rol, "NotaCreditoCompra", "Pagar");
            if (Validacion)
            {
                nota_credito_compra NCC = db.nota_credito_compra.Where(p => p.id == id).Select(p => p).Single();
                NCC.fecha_paga = DateTime.Today;
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
