using System;

namespace AQS.Api.Reading.Domain
{
    public class Reading : BaseEntity
    {
        public Guid DeviceId { get; set; }
        public ReadingType ReadingType { get; set; }
        public string Value { get; set; }
    }
}
