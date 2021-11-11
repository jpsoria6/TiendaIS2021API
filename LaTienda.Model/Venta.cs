using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaTienda.Model
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public double Monto { get; set; }
        public virtual ICollection<LineaDeVenta> LineaDeVentas { get; set; }
        public Empleado Empleado { get; set; }
        public Comprobante Factura { get; set; }
        public Cliente Cliente { get; set; }


        public Venta()
        {
            LineaDeVentas = new List<LineaDeVenta>();
        }

        public void AgregarLineaDeVenta(LineaDeVenta lineaDeVenta)
        {
            LineaDeVentas.Add(lineaDeVenta);
        }

        public bool ValidarVenta()
        {
            if (this.LineaDeVentas.Count < 0)
            {
                return false;
            }
            if (this.Empleado == null)
            {
                return false;
            }
            if (this.Cliente == null)
            {
                return false;
            }
            if(this.Monto < 0)
            {
                return false;
            }
            return true;
        }

        public void CalcularMonto()
        {
            Monto = LineaDeVentas.Sum( x => x.Precio * x.Cantidad);
        }
    }
    
    
    
}
