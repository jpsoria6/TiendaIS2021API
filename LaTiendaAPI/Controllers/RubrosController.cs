using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTienda.API.Features.Marcas;
using LaTienda.API.Features.Rubros;
using MediatR;

namespace LaTienda.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RubrosController : ControllerBase
    {
        private IMediator _mediator;
        public RubrosController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetRubrosQuery.QueryResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> GetRubros([FromHeader] GetRubrosQuery.Query cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateRubroCommand.CommandResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> CreateRubros([FromBody] CreateRubroCommand.Command cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }
    }
}
