using FluentValidation;
using LaTienda.API.Persistence;
using LaTienda.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LaTienda.API.Features.Marcas
{
    public class CreateMarcaCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public string Description { get; set; }
        }
        public class CommandResult
        {
            public int Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {

            public CommandValidator()
            {
                RuleFor(c => c.Description)
                    .NotEmpty().WithMessage("La descripcion no puede ser vacia")
                    .NotNull().WithMessage("La descripcion no puede ser nula");

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
                var marca = new Marca
                {
                    Descripcion = request.Description
                };
                _context.Marcas.Add(marca);
                await _context.SaveChangesAsync();

                return new CommandResult()
                {
                    Id = marca.Id
                };
            }
        }

    }
}
