using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    public class Tienda
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string DomicilioLegal { get; set; }
        public CondicionTributaria CondicionTributaria { get; set; }
        public virtual ICollection<Sucursal> Sucursales { get; set; }
    }
}
