using System;
using System.Collections.Generic;

namespace LaTienda.Model
{
    public class Empleado
    {
        public  int Id { get; set; }
        public int Legajo { get; set; }
        public string Nombre { get; set; }
        public string Pass { get; set; }
        public bool Autenticado { get; set; }
        public TipoEmpleado Tipo { get; set; }
        public Sucursal Sucursal { get; set; }
        public virtual  ICollection<Venta> Ventas { get; set; }

    }
}
