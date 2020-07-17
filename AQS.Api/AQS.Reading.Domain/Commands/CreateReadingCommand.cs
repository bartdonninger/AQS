using System;
using AQS.Api.Reading.Domain.Commands.Base;
using AQS.Api.Reading.Domain.Dtos;
using AQS.Api.Reading.Domain.Models;

namespace AQS.Api.Reading.Domain.Commands
{
    public class CreateReadingCommand : BaseCommand<ReadingDto>
    {
        public Guid DeviceId { get; set; }
        public ReadingType ReadingType { get; set; }
        public string Value { get; set; }
    }
}
