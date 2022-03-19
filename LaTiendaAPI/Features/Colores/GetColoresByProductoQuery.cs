using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LaTienda.API.Features.Colores
{
    public class GetColoresByProductoQuery
    {
        public class Query : IRequest<QueryResult>
        {
            public string CodigoProducto { get; set; }
        }

        public class QueryResult
        {
            public List<Color> Colores { get; set; } 
        }

        
       
        public class Handler : IRequestHandler<Query, QueryResult>
        {
            private TiendaContext _context;
            public Handler(TiendaContext context)
            {
                _context = context;
            }

            public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var colors = await _context.Colores
                    .Include(c => c.Stocks)
                    .ThenInclude(s => s.Producto)
                    .Where(c => c.Stocks.Any(s => s.Producto.Codigo.Equals(request.CodigoProducto)))
                    .ToListAsync();
                return new QueryResult()
                {
                    Colores = colors
                };
            }
        }
    }
}
