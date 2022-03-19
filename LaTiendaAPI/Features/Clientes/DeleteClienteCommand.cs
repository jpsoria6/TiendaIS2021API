using LaTienda.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Clientes
{
    public class DeleteClienteCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public string Cuit { get; set; }
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
                var cliente = await _context.Clientes
                    .FirstOrDefaultAsync(p => p.Cuit.Equals(request.Cuit));
                cliente.EstaBorrado = true;
                _context.Update(cliente);
                _context.SaveChanges();

                return new CommandResult()
                {
                    Success = true
                };
            }

        }
    }
}
