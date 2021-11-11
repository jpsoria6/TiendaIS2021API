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

namespace LaTienda.API.Features.Marcas
{
    public class GetMarcasQuery
    {
        public class Query : IRequest<QueryResult>
        {
            
        }

        public class QueryResult
        {
            public List<Marca> Marcas { get; set; } 
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
                var marcas = await _context.Marcas.ToListAsync();
                return new QueryResult()
                {
                    Marcas = marcas
                };
            }
        }
    }
}
