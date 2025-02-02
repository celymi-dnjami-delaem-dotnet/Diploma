using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Core.Entities;

namespace WebAPI.Infrastructure.Postgres.Configuration
{
    public class EpicConfiguration : IEntityTypeConfiguration<Epic>
    {
        public void Configure(EntityTypeBuilder<Epic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("EpicId");
            builder.Property(x => x.CreationDate).HasColumnType("timestamptz");
            builder
                .HasOne<Project>()
                .WithMany(e => e.Epics)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasIndex(x => x.ProjectId);
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}