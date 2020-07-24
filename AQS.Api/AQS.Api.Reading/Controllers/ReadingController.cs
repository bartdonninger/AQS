using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AQS.Api.Reading.Domain.Commands;
using AQS.Api.Reading.Domain.Dtos;
using AQS.Api.Reading.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AQS.Api.Reading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingController : ControllerBase
    {
        private readonly ILogger<ReadingController> _logger;
        private readonly IMediator _mediator;

        public ReadingController(ILogger<ReadingController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "test";
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<ReadingDto>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<ReadingDto>>> Get([FromQuery]GetReadingsQuery getReadingsQuery)
        //{
        //    var readingDtos = await _mediator.Send(getReadingsQuery);
            
        //    return Ok(readingDtos);
        //}

        [HttpPost]
        [ProducesResponseType(typeof(ReadingDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ReadingDto>> Create([FromBody] CreateReadingCommand createReadingCommand)
        {
            var readingDto = await _mediator.Send(createReadingCommand);

            _logger.LogDebug("Reading created");

            // Todo: What kind of location is required with the Created HTTP status?
            return Created("bla", readingDto);
        }
    }
}
