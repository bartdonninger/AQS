using System.Collections.Generic;
using AQS.Api.Reading.Domain.Dtos;
using AutoMapper;

namespace AQS.Api.Reading.Services.Mappers
{
    public interface IReadingMapper
    {
        ReadingDto MapReadingDto(Domain.Models.Reading reading);
        IEnumerable<ReadingDto> MapReadingDtos(IEnumerable<Domain.Models.Reading> readings);
    }

    public class ReadingMapper : IReadingMapper
    {
        private readonly IMapper _mapper;

        public ReadingMapper()
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Domain.Models.Reading, ReadingDto>();
            });

            _mapper = config.CreateMapper();
        }

        public ReadingDto MapReadingDto(Domain.Models.Reading reading)
        {
            return _mapper.Map<ReadingDto>(reading);
        }

        public IEnumerable<ReadingDto> MapReadingDtos(IEnumerable<Domain.Models.Reading> readings)
        {
            return _mapper.Map<List<ReadingDto>>(readings);
        }
    }
}
