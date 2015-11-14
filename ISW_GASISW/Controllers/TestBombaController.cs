using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISW_GASISW.Models;

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
            return View();
        }

        //
        // POST: /TestBomba/
        [HttpPost]
        public ActionResult Index(M_PeticionArduino MPA)
        {
            CARD.Test2(MPA);
            return View(MPA);
        }
    }
}
