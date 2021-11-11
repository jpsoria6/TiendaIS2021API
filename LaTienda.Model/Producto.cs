using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    public class Producto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public double MargenGanancia { get; set; }
        public double NetoGravado { get; set;}
        public double Iva { get; set; }
        public double PorcentajeIva { get; set; }
        public double PrecioVenta { get; set; }
        public Rubro Rubro { get; set; }
        public Marca Marca { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }




        public double GetNetoAgrabado()
        {
            return Costo + Costo * MargenGanancia;
        }

        public double GetIVA(double porcentajeIva)
        {
            return NetoGravado * porcentajeIva;
        }

        public double GetPrecioVenta()
        {
            return NetoGravado + Iva;
        }
    }
}
