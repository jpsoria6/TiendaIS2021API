using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    public class Comprobante
    {
        public int Id { get; set; }
        public TipoComprobante Tipo { get; set; }
        public string NumeroComprobante { get; set; }
        public Cliente Cliente { get; set; }

    }
}
