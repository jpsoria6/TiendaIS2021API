using AutoMapper;
using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Clientes
{
    public class GetClienteVenta
    {
        public class Query : IRequest<QueryResult>
        {
            public string Buscar { get; set; }
        }

        public class QueryResult
        {
            public Cliente Cliente { get; set; }
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

                var cliente = _context.Clientes.FirstOrDefault(x => x.Cuit.Contains(request.Buscar) || x.RazonSocial.Contains(request.Buscar) && x.EstaBorrado == false);

                return new QueryResult()
                {
                    Cliente = cliente
                };
            }

        }
    }
}
