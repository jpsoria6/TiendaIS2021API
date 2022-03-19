using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model.DTOS
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public double MargenGanancia { get; set; }
        public double NetoGravado { get; set; }
        public double Iva { get; set; }
        public double PrecioVenta { get; set; }
        public TipoTalle TipoTalle { get; set; }
        public MarcaDTO Marca { get; set; }
        public List<StockDTO> Stocks { get; set; }
        public RubroDTO Rubro { get; set; }
    }
}
