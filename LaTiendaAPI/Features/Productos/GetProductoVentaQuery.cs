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
    public class GetProductoVentaQuery
    {
        public class Query : IRequest<QueryResult>
        {
            public string CodigoProducto { get; set; }
            public int IdUsuario { get; set; }
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
                    .Include(p => p.Marca)
                    .FirstOrDefaultAsync(p => p.Codigo.Equals(request.CodigoProducto) 
                                              && p.EstaBorrado == false);

                var user = await _context.Empleados
                    .Include(e => e.Sucursal)
                    .FirstOrDefaultAsync(u => u.Id == request.IdUsuario);

                var stocks = await _context.Stocks
                    .Include(s => s.Talle)
                    .Include(s => s.Color)
                    .Include(s => s.Producto)
                    .Where(s => s.Sucursal.Id == user.Sucursal.Id && s.Producto.Id == producto.Id)
                    .ToListAsync();

                producto.Stocks = stocks;
                var result = _mapper.Map<ProductoDTO>(producto);
           
                return new QueryResult()
                {
                    Producto = result
                };
            }


           
        }
    }
}
