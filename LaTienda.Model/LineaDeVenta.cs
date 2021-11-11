using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    public class LineaDeVenta
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public Stock Stock { get; set; }
    }
}
