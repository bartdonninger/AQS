using System;
using System.Collections.Generic;
using System.Text;

namespace AQS.Api.Reading.Domain.Dtos
{
    public class ReadingDto
    {
        public Guid DeviceId { get; set; }
        public string ReadingType { get; set; }
        public string Value { get; set; }
    }
}
