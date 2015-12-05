using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISW_GASISW.Models;

namespace ISW_GASISW.Controllers
{
    public class C_Ingreso_Caja
    {
        private gasiswEntities db = new gasiswEntities();

        public void Ingreso(int caja,float montos,int venta)
        {
            moviementos_caja MC = new moviementos_caja();
            MC.CAJA_id = caja;
            MC.TIPO_MOVIMIENTO_id = 2;
            MC.fecha = DateTime.Today;
            MC.monto = montos;
            MC.M_VENTA_id = venta;

            db.moviementos_caja.Add(MC);
            db.SaveChanges();
        }       
    }
}