using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LaTienda.Model;
using LaTienda.Model.DTOS;

namespace LaTienda.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Producto, ProductoDTO>();
            CreateMap<Stock, StockDTO>();
            CreateMap<Talle, TalleDTO>();
            CreateMap<Color, ColorDTO>();
            CreateMap<Marca, MarcaDTO>();
            CreateMap<Stock, StockDTO>();
            CreateMap<Stock, StockCantDTO>();


        }
    }
}
