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
        public bool EstaBorrado { get; set; }
        public double NetoGravado { get { return Costo + Costo * MargenGanancia; } }
        public double Iva { get { return NetoGravado * (PorcentajeIva)/100; } }
        public double PorcentajeIva { get; set; }
        public TipoTalle TipoTalle { get; set; }
        public double PrecioVenta { get { return NetoGravado + Iva; } }
        public Rubro Rubro { get; set; }
        public Marca Marca { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

    }
}
