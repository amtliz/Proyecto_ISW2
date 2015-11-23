using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISW_GASISW.Controllers
{
    public class FacturacionController : Controller
    {
        //
        // GET: /Facturacion/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Facturacion/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}
