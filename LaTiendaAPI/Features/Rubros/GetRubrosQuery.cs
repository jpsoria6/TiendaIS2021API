using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LaTienda.API.Features.Rubros
{
    public class GetRubrosQuery
    {
        public class Query : IRequest<QueryResult>
        {

        }

        public class QueryResult
        {
            public List<RubroDTO> Rubros { get; set; }
        }

        public class RubroDTO
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
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
                var rubros = await _context.Rubros.ToListAsync();
                var rub = MapRubrosDtos(rubros);
                return new QueryResult()
                {
                    Rubros = rub
                };
            }

            public List<RubroDTO> MapRubrosDtos(List<Rubro> rubros)
            {
                var rubrosDto = new List<RubroDTO>();
                foreach (var r in rubros)
                {
                    rubrosDto.Add(new RubroDTO()
                    {
                        Id = r.Id,
                        Descripcion = r.Descripcion
                    });
                }

                return rubrosDto;
            }
        }
    }
}
