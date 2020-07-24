using System;
using System.Collections.Generic;
using AQS.Api.Reading.Domain.Dtos;

namespace AQS.Api.Reading.Domain.Queries
{
    public class GetReadingsQuery : BaseQuery<IEnumerable<ReadingDto>>
    {
        public Guid DeviceId { get; set; }
    }
}
