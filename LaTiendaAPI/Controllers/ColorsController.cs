using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTienda.API.Features.Colores;
using MediatR;

namespace LaTienda.API.Controllers
{
    [Route("api/[controller]/[action]")]

    [ApiController]
    public class ColorsController : ControllerBase
    {
        private IMediator _mediator;
        public ColorsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetColoresQuery.QueryResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> GetColores([FromHeader] GetColoresQuery.Query cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetColoresByProductoQuery.QueryResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> GetColoresByProducto([FromHeader] GetColoresByProductoQuery.Query cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(CreateColorCommand.CommandResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> CreateColor([FromBody] CreateColorCommand.Command cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

    }
}
