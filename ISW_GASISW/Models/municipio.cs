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
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    
    public partial class municipio
    {
        public municipio()
        {
            this.cliente = new HashSet<cliente>();
            this.empleado = new HashSet<empleado>();
            this.proveedor = new HashSet<proveedor>();
            this.sucursal = new HashSet<sucursal>();
        }
    
        public long id { get; set; }
        [Required]
        public string nombre { get; set; }
        public long DEPARTAMENTO_id { get; set; }
    
        public virtual ICollection<cliente> cliente { get; set; }
        public virtual departamento departamento { get; set; }
        public virtual ICollection<empleado> empleado { get; set; }
        public virtual ICollection<proveedor> proveedor { get; set; }
        public virtual ICollection<sucursal> sucursal { get; set; }
    }
}
