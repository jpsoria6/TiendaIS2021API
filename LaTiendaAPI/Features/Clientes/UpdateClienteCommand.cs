using FluentValidation;
using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Clientes
{
    public class UpdateClienteCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public int IdCliente { get; set; }
            public string Cuit { get; set; }
            public string RazonSocial { get; set; }
            public string Domicilio { get; set; }
            public CondicionTributaria CondicionTributaria { get; set; }
        }


        public class CommandResult
        {
            public int IdCliente { get; set; }
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
               
                var cliente = _context.Clientes.FirstOrDefault(p => p.Id == request.IdCliente);
                if (cliente != null)
                {

                    cliente.RazonSocial = request.RazonSocial;
                    cliente.Domicilio = request.Domicilio;
                    cliente.Cuit = request.Cuit;
                    cliente.CondicionTributaria = request.CondicionTributaria;
                    _context.Clientes.Update(cliente);
                    _context.SaveChanges();

                }
                return new CommandResult()
                {
                    IdCliente = cliente.Id
                };
            }
        }
    }
}
