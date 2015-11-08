using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISW_GASISW.Models;

namespace ISW_GASISW.Controllers
{
    public class Seguridad
    {
        ISW_GASISW.Models.gasiswEntities db = new Models.gasiswEntities();

        public bool ValidarAcceso(int rol, string area, string accion)
        {
            int area_id = Convert.ToInt16(db.formularios.Where(x => x.url == area).Select(x => x.id).Single());
            int accion_id = Convert.ToInt16(db.permisos.Where(x => x.url == accion).Select(x => x.id).Single());
            bool Validar = db.accesos.Any(x => x.FORMULARIOS_id == area_id && x.PERMISOS_id == accion_id && x.ROL_id == rol);
            return Validar;
        }
    }
}