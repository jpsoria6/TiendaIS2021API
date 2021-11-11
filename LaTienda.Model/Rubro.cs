using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    public class Rubro
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
