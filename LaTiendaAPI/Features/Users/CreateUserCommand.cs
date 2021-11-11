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

namespace LaTienda.API.Features.Users
{
    public class CreateUserCommand
    {
        public class Command : IRequest<CommandResult>
        {
            public string Nombre { get; set; }
            public int Legajo { get; set; }
            public string Password { get; set; }
            public TipoEmpleado Tipo { get; set; }
            
            //TODO : Agregar sucursal
        }

        public class CommandResult
        {
            public int Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {

            public CommandValidator()
            {
                RuleFor(c => c.Legajo)
                    .NotEmpty().WithMessage("El legajo no puede estar vacio")
                    .NotNull().WithMessage("El legajo no puede ser nulo");
                RuleFor(c => c.Password)
                    .NotEmpty().WithMessage("El password no puede estar vacio")
                    .NotNull().WithMessage("El password no puede ser nulo");

                RuleFor(c => c.Nombre)
                    .NotEmpty().WithMessage("El nombre no puede estar vacio")
                    .NotNull().WithMessage("El nombre no puede ser nulo");


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
                var empleado = new Empleado()
                {
                    Legajo = request.Legajo,
                    Nombre = request.Nombre,
                    Pass = request.Password,
                    Tipo = request.Tipo,
                    Autenticado = false
                };

                _context.Empleados.Add(empleado);
                _context.SaveChanges();

                return new CommandResult()
                {
                    Id = empleado.Id
                };
            }
        }
    }
}
