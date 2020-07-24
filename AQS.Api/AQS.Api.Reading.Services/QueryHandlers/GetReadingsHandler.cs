using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AQS.Api.Reading.DataAccess;
using AQS.Api.Reading.Domain.Dtos;
using AQS.Api.Reading.Domain.Queries;
using AQS.Api.Reading.Services.Mappers;
using MediatR;

namespace AQS.Api.Reading.Services.QueryHandlers
{
    public class GetReadingsHandler : IRequestHandler<GetReadingsQuery, IEnumerable<ReadingDto>>
    {
        private readonly ReadingContext _readingContext;
        private readonly IReadingMapper _readingMapper;

        public GetReadingsHandler(ReadingContext readingContext, IReadingMapper readingMapper)
        {
            _readingContext = readingContext ?? throw new ArgumentNullException(nameof(readingContext));
            _readingMapper = readingMapper ?? throw new ArgumentNullException(nameof(readingMapper));
        }

        public Task<IEnumerable<ReadingDto>> Handle(GetReadingsQuery request, CancellationToken cancellationToken)
        {
            var readings =_readingContext.Readings.Where(reading => reading.DeviceId == request.DeviceId).Take(1000).ToList();

            return Task.FromResult(_readingMapper.MapReadingDtos(readings));
        }
    }
}
