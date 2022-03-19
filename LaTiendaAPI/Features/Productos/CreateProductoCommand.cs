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

namespace LaTienda.API.Features.Productos
{
    public class CreateProductoCommand
    {
        public class Command : IRequest<CommandResult>
        {
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
                var marca =await _context.Marcas.FirstOrDefaultAsync(m => m.Id == request.IdMarca);
                var rubro = await _context.Rubros.FirstOrDefaultAsync(r => r.Id == request.IdRubro);

                var producto = new Producto()
                {
                    Codigo = request.CodigoProducto,
                    Costo = request.Costo,
                    Descripcion = request.Descripcion,
                    MargenGanancia = request.MargenGanancia,
                    Marca = marca,
                    Rubro = rubro,
                    TipoTalle = request.TipoTalle,
                    PorcentajeIva = request.Iva,
                    EstaBorrado = false
                };

                _context.Productos.Add(producto);
                _context.SaveChanges();

                return new CommandResult()
                {
                    IdProducto = producto.Id
                };
            }
        }
    }
}
