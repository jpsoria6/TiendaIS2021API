using FluentValidation;
using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Stocks
{
    public class AgregarStockCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public int IdTalle { get; set; }
            public int IdColor { get; set; }
            public string CodigoProducto { get; set; }
            public int IdSucursal { get; set; }
            public int Cantidad { get; set; }
        }


        public class CommandResult
        {
            public bool Success { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private TiendaContext _context;
            public CommandValidator(TiendaContext context)
            {
                
            }
        }

        public class Handler : IRequestHandler<Command, CommandResult>
        {
            private TiendaContext _context;
            public Handler(TiendaContext context)
            {
                _context = context;
            }

            public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var stock = await _context.Stocks
                    .Include(s => s.Producto)
                    .Include(s => s.Talle)
                    .Include(s => s.Color)
                    .Include(s => s.Sucursal)
                    .FirstOrDefaultAsync(st => st.Sucursal.Id == request.IdSucursal 
                                        && st.Producto.Codigo == request.CodigoProducto
                                        && st.Talle.Id == request.IdTalle
                                        && st.Color.Id == request.IdColor);
                if(stock != null)
                {
                    stock.Cantidad += request.Cantidad;
                    _context.Stocks.Update(stock);
                    _context.SaveChanges();
                    return new CommandResult()
                    {
                        Success = true
                    };
                }
                else
                {
                    var color = _context.Colores.FirstOrDefault(c => c.Id == request.IdColor);
                    var talle = _context.Talles.FirstOrDefault(c => c.Id == request.IdTalle);
                    var sucursal = _context.Sucursales.FirstOrDefault(c => c.Id == request.IdSucursal);
                    var producto = _context.Productos.FirstOrDefault(c => c.Codigo.Equals(request.CodigoProducto));
                    var s = new Stock()
                    {
                        Color = color,
                        Talle = talle,
                        Sucursal = sucursal,
                        Producto = producto,
                        Cantidad = request.Cantidad
                    };
                    _context.Stocks.Add(s);
                    _context.SaveChanges();
                    return new CommandResult()
                    {
                        Success = true
                    };
                }
                
            }
        }
    }
}
