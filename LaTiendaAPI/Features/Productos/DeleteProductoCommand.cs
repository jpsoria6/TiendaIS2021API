using LaTienda.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Productos
{
    public class DeleteProductoCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public string CodigoProducto { get; set; }
        }

        public class CommandResult
        {
            public bool Success { get; set; }
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
                var producto = await _context.Productos
                    .FirstOrDefaultAsync(p => p.Codigo.Equals(request.CodigoProducto));
                producto.EstaBorrado = true;
                _context.Update(producto);
                _context.SaveChanges();

                return new CommandResult()
                {
                    Success = true
                };
            }

        }
    }

}
