using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AQS.Api.Reading.DataAccess.Queries
{
    public static class ReadingQuery
    {
        public static async Task<Domain.Models.Reading> GetReadingByIdAsync(this DbSet<Domain.Models.Reading> readingDbSet,
            Expression<Func<Domain.Models.Reading, dynamic>> projection, Guid readingId)
        {
            return await readingDbSet.Where(reading => reading.Id == readingId)
                .Select(projection)
                .FirstOrDefaultAsync();
        }
    }
}
