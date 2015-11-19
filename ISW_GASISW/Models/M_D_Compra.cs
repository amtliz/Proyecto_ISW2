using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISW_GASISW.Models
{
    public class M_D_Compra
    {
        public string[] Cant { get; set; }
        public string[] id { get; set; }
        public string[] cost { get; set; }
        public int TipoCompra { get; set; }
        public List<d_compra> Lista { get; set; }
    }

    
}