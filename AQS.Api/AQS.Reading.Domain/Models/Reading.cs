using System;

namespace AQS.Api.Reading.Domain.Models
{
    public class Reading : BaseEntity
    {
        public Reading(Guid deviceId, ReadingType readingType, string value)
        {
            DeviceId = deviceId;
            ReadingType = readingType;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Guid DeviceId { get; set; }
        public ReadingType ReadingType { get; set; }
        public string Value { get; set; }
    }
}
