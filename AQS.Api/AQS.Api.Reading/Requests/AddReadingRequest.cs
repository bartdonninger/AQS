using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AQS.Api.Reading.Requests
{
    public class AddReadingRequest
    {
        public Guid DeviceId { get; set; }
        public string ReadingType { get; set; }
        public string Value { get; set; }
    }
}
