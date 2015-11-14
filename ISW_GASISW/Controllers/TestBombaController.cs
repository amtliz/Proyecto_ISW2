using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISW_GASISW.Controllers
{
    public class TestBombaController : Controller
    {
        //Llamando a la clase Arduino
        C_Arduino CARD = new C_Arduino();

        //
        // GET: /TestBomba/
        public ActionResult Index()
        {
            CARD.Test();
            return View();
        }

    }
}
