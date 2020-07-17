using AQS.Api.Reading.Domain.Dtos;
using AutoMapper;

namespace AQS.Api.Reading.Services.Mappers
{
    public interface IReadingMapper
    {
        ReadingDto MapReadingDto(Domain.Models.Reading reading);
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
    }
}
