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

namespace LaTienda.API.Features.Ventas
{
    public class CreateVentaCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public int IdEmpleado { get; set; }
            public List<LineaDeVentaDTO> LineaDeVentas { get; set; }
        }

        public class LineaDeVentaDTO
        {
            public int Cantidad { get; set; }
            public int IdTalle { get; set; }
            public int IdColor { get; set; }
            public string CodigoProducto { get; set; }
        }

        public class CommandResult
        {
           public int IdVenta { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {

            public CommandValidator()
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
                var empleado = await _context.Empleados
                    .Include(e => e.Sucursal)
                    .FirstOrDefaultAsync(e => e.Id == request.IdEmpleado);
                var ldvs = await MapLineaDeVentas(request.LineaDeVentas,empleado.Sucursal.Id);
                var venta = new Venta()
                {
                    Empleado = empleado,
                    LineaDeVentas = ldvs,
                    FechaHora = DateTime.Now
                };
                _context.Ventas.Add(venta);
                _context.SaveChanges();
                return new CommandResult()
                {
                    IdVenta = venta.Id
                };
            }

            public async Task<List<LineaDeVenta>> MapLineaDeVentas(List<LineaDeVentaDTO> lineaDeVentaDtos,int idSucursal)
            {
                var lineaDeVentas = new List<LineaDeVenta>();
                foreach (var ldv in lineaDeVentaDtos)
                {
                    try
                    {
                        var stock = await _context.Stocks
                            .FirstOrDefaultAsync(s =>
                                s.Color.Id == ldv.IdColor && s.Talle.Id == ldv.IdTalle &&
                                s.Producto.Codigo.Equals(ldv.CodigoProducto) &&
                                s.Sucursal.Id == idSucursal);
                        stock.DisminuirCantidad(ldv.Cantidad);
                        var ldvInstance = new LineaDeVenta()
                        {
                            Cantidad = ldv.Cantidad,
                            Stock = stock
                        };
                        lineaDeVentas.Add(ldvInstance);
                    }
                    catch (Exception e)
                    {

                    }
                    
                }
                return lineaDeVentas;
            }
        }
    }
}
