using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skolar.Domain.Todos;

namespace Skolar.Infrastructure.Configurations;

internal sealed class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
 public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("todos");

        builder.HasKey(t => t.Id);

        builder.OwnsOne(tb=> tb.Title, titleBuilder =>
        {
            titleBuilder.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("title");
        });


       builder.OwnsOne(tb => tb.Description, descriptionBuilder =>
        {
            descriptionBuilder.Property(d => d.Value)
                .HasMaxLength(256)
                .HasColumnName("description");
        });

        builder.OwnsOne(tb => tb.Metadata, metadataBuilder =>
        {
            metadataBuilder.Property(m => m.Priority)
                .IsRequired()
                .HasColumnName("priority")
                .HasConversion<string>();


            metadataBuilder.Property(m => m.IsCompleted)
                .HasColumnName("is_Completed");

            metadataBuilder.Property(m => m.DueDate)
                .HasColumnName("due_date");
        });

        builder.Property(t => t.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(t => t.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(t => t.CompletedAt)
            .HasColumnName("completed_at");

    }
}
