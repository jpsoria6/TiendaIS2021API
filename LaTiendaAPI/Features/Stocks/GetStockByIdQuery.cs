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

namespace LaTienda.API.Features.Stocks
{
    public class GetStockByIdQuery
    {
        public class Query : IRequest<QueryResult>
        {
            public string CodigoProducto { get; set; }
            public int IdEmpleado { get; set; }
        }

        public class QueryResult
        {
            public List<StockCantDTO> Stocks { get; set; }
        }

        public class Handler : IRequestHandler<Query, QueryResult>
        {
            private TiendaContext _context;
            private readonly IMapper _mapper;

            public Handler(TiendaContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<QueryResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var stocks = _context.Stocks
                    .Include(s => s.Talle)
                    .Include(s => s.Color)
                    .Include(s => s.Sucursal)
                    .Where(s => s.Producto.Codigo.Equals(request.CodigoProducto) && s.Sucursal.Empleados.Any(e => e.Id == request.IdEmpleado));
                //TODO : De esta forma no estamos permitiendo que se puedan comprar mas productos de los que hay stockeados
                //Opcion 1: Mostrar un input para permitir agregar en caso de que no apareca, dar opcion al usuario para hacerlo visible o no con un checkbox
                var result = _mapper.Map<List<StockCantDTO>>(stocks);
                return new QueryResult()
                {
                    Stocks = result
                };
            }
        }
    }
}
