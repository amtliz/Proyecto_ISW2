//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISW_GASISW.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class inventario
    {
        public long id { get; set; }
        public long PRODUCTO_id { get; set; }
        public int cantidad { get; set; }
        public System.DateTime fecha { get; set; }
        public long KARDEX_id { get; set; }
        public float precio_u { get; set; }
    
        public virtual kardex kardex { get; set; }
        public virtual producto producto { get; set; }
    }
}