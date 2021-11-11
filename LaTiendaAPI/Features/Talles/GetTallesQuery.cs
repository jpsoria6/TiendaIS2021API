using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using LaTienda.API.Persistence;
using LaTienda.Model;
using LaTienda.Model.DTOS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LaTienda.API.Features.Marcas
{
    public class GetTallesQuery
    {
        public class Query : IRequest<QueryResult>
        {
            public int ColorId { get; set; }
            public string CodigoProducto { get; set; }
        }

        public class QueryResult
        {
            public List<TalleDTO> Talles { get; set; }
        }

     
       
        public class Handler : IRequestHandler<Query, QueryResult>
        {
            private TiendaContext _context;
            private IMapper _mapper;
            public Handler(TiendaContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var talles = _context.Talles
                    .Include(s => s.Stocks)
                    .ThenInclude(p => p.Producto)
                    .Where(t => t.Stocks.Any(s => s.Producto.Codigo.Equals(request.CodigoProducto) && s.Color.Id == request.ColorId))
                    .ToList();
                var result = _mapper.Map<List<TalleDTO>>(talles);
                return new QueryResult()
                {
                    Talles = result
                };
            }

         
        }
    }
}
