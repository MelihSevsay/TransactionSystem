using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionSystem.Domain.Entities;

namespace TransactionSystem.Infrastructure.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.UserId)
            .IsRequired();

        builder.Property(t => t.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.TransactionType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(t => t.CreationDateTime)
            .IsRequired()
             .HasDefaultValueSql("GETUTCDATE()");

        //No index on TransactionType: cardinality is too low.
        builder.HasIndex(t => t.Amount);
        builder.HasIndex(t => t.UserId);
    }
}
