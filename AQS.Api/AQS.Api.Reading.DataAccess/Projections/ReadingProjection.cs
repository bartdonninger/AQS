using System;
using System.Linq.Expressions;

namespace AQS.Api.Reading.DataAccess.Projections
{
    public static class ReadingProjection
    {
        public static Expression<Func<Domain.Models.Reading, dynamic>> Reading
        {
            get
            {
                return reading => new
                {
                    reading.Id,
                    reading.CreatedDateUtc,
                    reading.UpdatedDateUtc,
                    reading.DeviceId,
                    reading.ReadingType,
                    reading.Value
                };
            }
        }
    }
}
