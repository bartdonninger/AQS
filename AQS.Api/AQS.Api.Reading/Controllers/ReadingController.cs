using System.Net;
using AQS.Api.Reading.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AQS.Api.Reading.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingController : ControllerBase
    {

        private readonly ILogger<ReadingController> _logger;

        public ReadingController(ILogger<ReadingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "tester de test tweemaal";
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public string Get([FromBody] AddReadingRequest addReadingRequest)
        {
            // Todo: save request to database (repository pattern or cqrs?)

            return "tester de test tweemaal";
        }
    }
}
