using System;
using System.Collections.Generic;
using System.Text;

namespace LaTienda.Model.DTOS
{
    public class TalleDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public TipoTalle TipoTalle { get; set; }
    }
}
