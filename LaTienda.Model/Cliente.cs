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
        public bool EstaBorrado { get; set; }
        public CondicionTributaria CondicionTributaria { get; set; }

        public TipoComprobante GetTipoComprobante()
        {
            switch (CondicionTributaria)
            {
                case CondicionTributaria.ResponsableInscripto:
                    return TipoComprobante.FacturaA;
                case CondicionTributaria.Monotributo:
                    return TipoComprobante.FacturaA;
                case CondicionTributaria.Exento:
                    return TipoComprobante.FacturaB;
                case CondicionTributaria.NoResponsable:
                    return TipoComprobante.FacturaB;
                case CondicionTributaria.ConsumidorFinal:
                    return TipoComprobante.FacturaB;
                default:
                    return TipoComprobante.FacturaB;
            }
        }

    }
}
