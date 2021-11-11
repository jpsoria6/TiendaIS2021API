using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model.DTOS
{
    public class StockDTO
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public TalleDTO Talle { get; set; }
        public ColorDTO Color { get; set; }
    }
}
