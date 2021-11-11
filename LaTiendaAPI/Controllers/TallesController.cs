﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTienda.API.Features.Marcas;
using MediatR;

namespace LaTienda.API.Controllers
{
    [Route("api/[controller]/[action]")]
  
    [ApiController]
    public class TallesController : ControllerBase
    {
        private IMediator _mediator;
        public TallesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetTallesQuery.QueryResult), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> GetTalles([FromHeader] GetTallesQuery.Query cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }
    }
}
