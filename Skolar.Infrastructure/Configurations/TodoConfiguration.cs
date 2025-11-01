using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skolar.Domain.Todos;
using Skolar.Domain.Todos.ValueObjects;

namespace Skolar.Infrastructure.Configurations;

internal sealed class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
 public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("todos");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
               .ValueGeneratedNever();

        builder.Property(t => t.Title)
                 .HasConversion(
                     title => title.Value,                    
                     value => TodoTitle.Create(value).Value)          
                 .HasColumnName("title")
                 .HasMaxLength(200)
                 .IsRequired();

        builder.Property(t => t.Description)
                .HasConversion(
                    description => description == null ? null : description.Value,
                    value => value == null ? null : new TodoDescription(value))
                .HasColumnName("description")
                .HasMaxLength(200);

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
