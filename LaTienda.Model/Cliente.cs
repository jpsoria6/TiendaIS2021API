using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public string Domicilio { get; set; }
        public CondicionTributaria CondicionTributaria { get; set; }
    }
}
