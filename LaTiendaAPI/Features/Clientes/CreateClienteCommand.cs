using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Clientes
{
    public class CreateClienteCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public string Cuit { get; set; }
            public string RazonSocial { get; set; }
            public string Domicilio { get; set; }
            public CondicionTributaria CondicionTributaria { get; set; }
        }

        public class CommandResult
        {
            public int IdCliente {get;set;}
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

                var cliente = new Cliente()
                {
                    Cuit = request.Cuit,
                    RazonSocial = request.RazonSocial,
                    CondicionTributaria = request.CondicionTributaria,
                    Domicilio = request.Domicilio,
                    EstaBorrado = false
                };

                _context.Clientes.Add(cliente);
                _context.SaveChanges();

                return new CommandResult()
                {
                    IdCliente = cliente.Id
                };
            }
        }
    }
}
