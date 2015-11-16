using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISW_GASISW.Models
{
    public class M_M_Orden_Compra
    {
        public int proveedor { get; set; }
        public List<m_orden_compra> LMOC { get; set; }
    }

}