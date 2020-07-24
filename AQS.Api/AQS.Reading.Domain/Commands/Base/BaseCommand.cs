using MediatR;

namespace AQS.Api.Reading.Domain.Commands.Base
{
    public abstract class BaseCommand<T> : IRequest<T> where T : class
    {
    }
}
