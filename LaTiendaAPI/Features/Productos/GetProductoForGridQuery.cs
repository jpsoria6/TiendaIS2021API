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
    public class GetProductoForGridQuery
    {
        public class Query : IRequest<QueryResult>
        {
        }

        public class QueryResult
        {
            public List<ProductoDTO> Producto { get; set; }
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
                var productos = await _context.Productos
                    .Include(p => p.Stocks).ThenInclude(s => s.Talle)
                    .Include(p => p.Stocks).ThenInclude(s => s.Color)
                    .Include(p => p.Marca)
                    .Where(p => p.EstaBorrado == false)
                    .ToListAsync();

                var result = _mapper.Map<List<ProductoDTO>>(productos);
                return new QueryResult()
                {
                    Producto = result
                };
            }
        }
    }
}
