using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LaTienda.API.Persistence;
using LaTienda.Model;
using LaTienda.Model.DTOS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LaTienda.API.Features.Productos
{
    public class GetProductoByIdQuery
    {
        public class Query : IRequest<QueryResult>
        {
            public string CodigoProducto { get; set; }
        }

        public class QueryResult
        {
            public ProductoDTO Producto { get; set; }
        }

        public class Handler : IRequestHandler<Query, QueryResult>
        {
            private TiendaContext _context;
            IMapper _mapper;
            public Handler(TiendaContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var producto = await _context.Productos
                    .Include(p => p.Stocks).ThenInclude(s => s.Talle)
                    .Include(p => p.Stocks).ThenInclude(s => s.Color)
                    .Include(p => p.Marca)
                    .FirstOrDefaultAsync(p => p.Codigo.Equals(request.CodigoProducto));

                var result = _mapper.Map<ProductoDTO>(producto);
                return new QueryResult()
                {
                    Producto = result
                };
            }


            public ProductoDTO MapProductoDto(Producto producto)
            {
                var dto = new ProductoDTO()
                {
                    Id = producto.Id,
                    Codigo = producto.Codigo,
                    Costo = producto.Costo,
                    Descripcion = producto.Descripcion,
                    Iva = producto.Iva,
                    MargenGanancia = producto.MargenGanancia,
                    NetoGravado = producto.NetoGravado,
                    PrecioVenta = producto.PrecioVenta,
                   // Marca = producto.Marca,
                    Stocks = MapStockDtos(producto.Stocks)
                };

                return dto;
            }

            public List<StockDTO> MapStockDtos(ICollection<Stock> stocks)
            {
                var stocksDTO = new List<StockDTO>();
                foreach (var s in stocks)
                {
                    stocksDTO.Add(new StockDTO()
                    {
                        Id = s.Id,
                        Cantidad = s.Cantidad,
                       // Color = s.Color,
                        //Talle = s.Talle
                    });   
                }

                return stocksDTO;
            }
        }
    }
}
