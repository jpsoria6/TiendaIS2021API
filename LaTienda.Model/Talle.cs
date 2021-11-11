using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    public class Talle
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

    }
}
