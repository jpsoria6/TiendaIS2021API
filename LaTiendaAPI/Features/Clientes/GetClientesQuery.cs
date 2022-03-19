using AutoMapper;
using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Clientes
{
    public class GetClientesQuery
    {
        public class Query : IRequest<QueryResult>
        {
            public string Buscar { get; set; }
        }

        public class QueryResult
        {
            public List<Cliente> Clientes { get; set; }
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
                var clientes = new List<Cliente>();
                if (!string.IsNullOrEmpty(request.Buscar))
                {
                    clientes = await _context.Clientes
                                        .Where(c => (c.Cuit.Contains(request.Buscar) || c.RazonSocial.Contains(request.Buscar)) && c.EstaBorrado == false)
                                        .ToListAsync();
                }
                else
                {
                    clientes = await _context.Clientes.Where(x => x.EstaBorrado == false).ToListAsync();
                }

                return new QueryResult()
                {
                    Clientes = clientes
                };
            }
        }
    }
}
