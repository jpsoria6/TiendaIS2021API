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

namespace LaTienda.API.Features.Productos
{
    public class UpdateProductoCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public int Id { get; set; }
            public string CodigoProducto { get; set; }
            public string Descripcion { get; set; }
            public double Costo { get; set; }
            public int IdMarca { get; set; }
            public double MargenGanancia { get; set; }
            public int IdRubro { get; set; }
            public double Iva { get; set; }
            public TipoTalle TipoTalle { get; set; }
        }


        public class CommandResult
        {
            public int IdProducto { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private TiendaContext _context;
            public CommandValidator(TiendaContext context)
            {
                _context = context;
                RuleFor(c => c.CodigoProducto)
                    .Must(p => {
                        var existe = _context.Productos.Any(prod => prod.Codigo.Equals(p));
                        return !existe;
                    })
                    .WithMessage("Codigo Existente");
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
                var marca = await _context.Marcas.FirstOrDefaultAsync(m => m.Id == request.IdMarca);
                var rubro = await _context.Rubros.FirstOrDefaultAsync(r => r.Id == request.IdRubro);

                var producto = _context.Productos.FirstOrDefault(p => p.Id == request.Id);
                if(producto != null)
                {

                producto.Codigo = request.CodigoProducto;
                producto.Costo = request.Costo;
                producto.Descripcion = request.Descripcion;
                producto.MargenGanancia = request.MargenGanancia;
                producto.Marca = marca;
                producto.Rubro = rubro;
                producto.TipoTalle = request.TipoTalle;
                producto.PorcentajeIva = request.Iva;
                _context.Productos.Update(producto);
                _context.SaveChanges();

                }
                return new CommandResult()
                {
                    IdProducto = producto.Id
                };
            }
        }
    }
}
