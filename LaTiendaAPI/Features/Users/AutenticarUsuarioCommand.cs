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
    public class AutenticarUsuarioCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public int Legajo { get; set; }
            public string Password { get; set; }
        }

        public class CommandResult
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public bool estaAutenticado { get; set; }
            public string TipoUsuario { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
           
            public CommandValidator( )
            {
                RuleFor(c => c.Legajo)
                    .NotEmpty().WithMessage("El legajo no puede estar vacio")
                    .NotNull().WithMessage("El legajo no puede ser nulo");
                RuleFor(c => c.Password)
                    .NotEmpty().WithMessage("El password no puede estar vacio")
                    .NotNull().WithMessage("El password no puede ser nulo");

                
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
                    if (!empleado.Autenticado && empleado.Pass.Equals(request.Password))
                    {
                        empleado.Autenticado = true;
                        _context.SaveChanges();
                        return new CommandResult()
                        {
                            Id = empleado.Id,
                            Nombre = empleado.Nombre,
                            estaAutenticado = true,
                            TipoUsuario = empleado.Tipo.ToString().ToLower()
                        };
                    }
                }
                return new CommandResult()
                {
                    Nombre = "",
                    estaAutenticado = false
                };

            }
        }
    }

}

