using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LaTienda.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LaTienda.API.Features.Users
{
    public class CerrarSesionCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public int Legajo { get; set; }
        }

        public class CommandResult
        {
            public bool Success { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
           
            public CommandValidator( )
            {
                RuleFor(c => c.Legajo)
                    .NotEmpty().WithMessage("El legajo no puede estar vacio")
                    .NotNull().WithMessage("El legajo no puede ser nulo");
                
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
                var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Legajo == request.Legajo);
                if (empleado != null)
                {
                    if (empleado.Autenticado)
                    {
                        empleado.Autenticado = false;
                        _context.SaveChanges();
                        return new CommandResult()
                        {
                            Success = true
                        };
                    }
                }
                return new CommandResult()
                {
                    Success = false
                };

            }
        }
    }

}

