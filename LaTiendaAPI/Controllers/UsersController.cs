using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTienda.API.Features.Users;
using MediatR;

namespace LaTienda.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AutenticarUsuarioCommand.CommandResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> AutenticarUsuario([FromBody] AutenticarUsuarioCommand.Command cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserCommand.CommandResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> CreateUsuario([FromBody] CreateUserCommand.Command cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        
        [HttpPost]
        [ProducesResponseType(typeof(CerrarSesionCommand.CommandResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> CerrarSesion([FromBody] CerrarSesionCommand.Command cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }
    }
}
