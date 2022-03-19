using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTienda.API.Features.Stocks;
using MediatR;

namespace LaTienda.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(typeof(GetStockByIdQuery.QueryResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> GetStock([FromHeader] GetStockByIdQuery.Query cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AgregarStockCommand.CommandResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> AgregarStock([FromBody] AgregarStockCommand.Command cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }
    }
}
