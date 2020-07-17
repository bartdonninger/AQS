using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AQS.Api.Reading.DataAccess.Configurations
{
    public class ReadingConfiguration : BaseEntityConfiguration<Domain.Models.Reading>, IEntityTypeConfiguration<Domain.Models.Reading>
    {
        public new void Configure(EntityTypeBuilder<Domain.Models.Reading> builder)
        {
            base.Configure(builder);
        }
    }
}
