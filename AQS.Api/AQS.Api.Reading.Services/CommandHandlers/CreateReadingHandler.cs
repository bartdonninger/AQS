using System;
using System.Threading;
using System.Threading.Tasks;
using AQS.Api.Reading.DataAccess;
using AQS.Api.Reading.Domain.Commands;
using AQS.Api.Reading.Domain.Dtos;
using AQS.Api.Reading.Services.Mappers;
using MediatR;

namespace AQS.Api.Reading.Services.CommandHandlers
{
    public class CreateReadingHandler : IRequestHandler<CreateReadingCommand, ReadingDto>
    {
        private readonly ReadingContext _readingContext;
        private readonly IReadingMapper _readingMapper;

        public CreateReadingHandler(ReadingContext readingContext, IReadingMapper readingMapper)
        {
            _readingContext = readingContext ?? throw new ArgumentNullException(nameof(readingContext));
            _readingMapper = readingMapper ?? throw new ArgumentNullException(nameof(readingMapper));
        }

        public Task<ReadingDto> Handle(CreateReadingCommand command, CancellationToken cancellationToken)
        {
            var reading = new Domain.Models.Reading(command.DeviceId, command.ReadingType, command.Value);

             _readingContext.Readings.Add(reading);

            return Task.FromResult(_readingMapper.MapReadingDto(reading));
        }
    }
}
