using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Sucursales
{
    public class GetSucursalQuery
    {
        public class Query : IRequest<QueryResult>
        {

        }

        public class QueryResult
        {
            public List<Sucursal> Sucursales { get; set; }
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
                var sucursales = await _context.Sucursales.ToListAsync();
                return new QueryResult()
                {
                    Sucursales = sucursales
                };
            }
        }
    }
}
