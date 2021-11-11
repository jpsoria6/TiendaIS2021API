using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    public class Stock
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public Producto Producto { get; set; }
        public Color Color { get; set; }
        public Talle Talle { get; set; }
        public Sucursal Sucursal { get; set; }


        public void DisminuirCantidad(int cantidad)
        {
            this.Cantidad -= cantidad;
        }
    }
}
