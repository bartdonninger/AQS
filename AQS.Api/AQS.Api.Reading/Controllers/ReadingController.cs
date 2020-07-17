using System;
using System.Net;
using System.Threading.Tasks;
using AQS.Api.Reading.DataAccess;
using AQS.Api.Reading.Domain.Commands;
using AQS.Api.Reading.Domain.Dtos;
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

        public ReadingController(ILogger<ReadingController> logger, IMediator mediator, ReadingContext context)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public string Get()
        {
            // Todo: return readings
            return "tester de test tweemaal";
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReadingDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ReadingDto>> Create([FromBody] CreateReadingCommand addReadingRequest)
        {
            var readingDto = await _mediator.Send(addReadingRequest);

            _logger.LogDebug("Reading created");

            // Todo: What kind of location is required with the Created HTTP status?
            return Created("bla",readingDto);
        }
    }
}
