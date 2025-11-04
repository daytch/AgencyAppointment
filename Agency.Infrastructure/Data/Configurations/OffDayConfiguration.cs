using Agency.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agency.Infrastructure.Data.Configurations;

public class OffDayConfiguration : IEntityTypeConfiguration<OffDay>
{
    public void Configure(EntityTypeBuilder<OffDay> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Reason).HasMaxLength(300);
    }
}
