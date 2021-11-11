using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
   public class Sucursal
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
