using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISW_GASISW.Models;

namespace ISW_GASISW.Controllers
{
    public class GasolinaController : Controller
    {
        private gasiswEntities db = new gasiswEntities();
        //Llamando a la clase Arduino
        C_Arduino CARD = new C_Arduino();

        //
        // GET: /Gasolina/
        public ActionResult Index(string mensaje = "")
        {
            ViewBag.Estacion1 = db.estacion.Where(p => p.numero == 1).ToList();
            ViewBag.Estacion2 = db.estacion.Where(p => p.numero == 2).ToList();
            ViewBag.Estaciones = new SelectList(new[]
                                    {
                                        new { Id = "0", Name = "Seleccione una estacion"},
                                        new { Id = "1", Name = "Estacion #1" },
                                        new { Id = "2", Name = "Estacion #2" },
                                    }, "Id", "Name");
            ViewBag.TiposGas = new SelectList(new[]
                                    {
                                        new { Id = "0", Name = "Seleccione un combustible"},
                                        new { Id = "1", Name = "Regular" },
                                        new { Id = "2", Name = "Disel" },
                                    }, "Id", "Name");
            ViewBag.Mensaje = mensaje;
            return View();
        }

        //
        // POST: /Gasolina/
        [HttpPost]
        public ActionResult Index(M_PeticionArduino MPA)
        {
            //Validar que haya gasolina del tipo seleccionado
            int est = Convert.ToInt16(MPA.estacion);
            int tipo = Convert.ToInt16(MPA.tipo);
            int cant = Convert.ToInt16(MPA.giro);

            float cantidadActual = db.estacion.Where(p => p.numero == est && p.tipo == tipo).Select(p => p.cantidad).Single();
            if(cantidadActual>float.Parse(MPA.giro))
            {
                //hacerlo
                if (MPA.tipo == "1")        //Regular
                {
                    //Arbitrariamente se selecciono grados menores de 90
                    float giro = float.Parse(MPA.giro)*10;
                    float movimiento = 90 - giro;
                    MPA.giro = movimiento.ToString();
                    CARD.EnviarPeticion(MPA);
                }
                else if(MPA.tipo == "2")    //Disel
                {
                    //Arbitrariamente se selecciono grados mayores de 90
                    float giro = float.Parse(MPA.giro)*10;
                    float movimiento = 90 + giro;
                    MPA.giro = movimiento.ToString();
                    CARD.EnviarPeticion(MPA);
                }

                // Actualizando BD
                estacion E = db.estacion.Where(p => p.numero == est && p.tipo == tipo).Single();
                E.cantidad = E.cantidad - cant;
                db.SaveChanges();

                //Redireccionar Exito
                return RedirectToAction("Index");
            }
            else
            {
                //Escribir Error
                string mensaje = "Lo sentimos no hay suficiente combustible de ese tipo en esta estacion";
                return RedirectToAction("Index", new { mensaje });
            }            
        }

    }
}
