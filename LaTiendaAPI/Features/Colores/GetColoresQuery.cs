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
    public class GetColoresQuery
    {
        public class Query : IRequest<QueryResult>
        {
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
                    .ToListAsync();
                return new QueryResult()
                {
                    Colores = colors
                };
            }
        }
    }
}
