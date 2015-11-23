using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISW_GASISW.Models
{
    public class M_Nota_Credito_Venta
    {
        public m_venta MV { get; set; }
        public string plazo { get; set; }
        public int cliente { get; set; }
    }
}