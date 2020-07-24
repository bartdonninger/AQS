using MediatR;

namespace AQS.Api.Reading.Domain.Queries
{
    public abstract class BaseQuery<T> : IRequest<T>
    {
    }
}
