using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISW_GASISW.Models;

namespace ISW_GASISW.Controllers
{
    public class C_Ajuste_Invetario
    {
        private gasiswEntities db = new gasiswEntities();

        public int guardarLote(int pro, int sec)
        {
            int contador = db.lote.Where(p => p.PRODUCTO_id == pro).Count();
            contador = contador + 1;
            lote L = new lote();
            L.nombre = contador.ToString();
            L.PRODUCTO_id = pro;
            L.SECCION_BODEGA_id = sec;

            db.lote.Add(L);
            db.SaveChanges();

            int id = Convert.ToInt16(db.lote.Max(p => p.id));
            return id;
        }
    }
}