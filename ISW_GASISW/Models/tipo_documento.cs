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
    
    public partial class tipo_documento
    {
        public tipo_documento()
        {
            this.facturacion = new HashSet<facturacion>();
        }
    
        public long id { get; set; }
        public string nombre { get; set; }
    
        public virtual ICollection<facturacion> facturacion { get; set; }
    }
}