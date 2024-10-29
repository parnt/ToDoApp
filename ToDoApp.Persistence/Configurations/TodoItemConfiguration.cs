using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Infrastructure.Entities;

namespace ToDoApp.Persistence.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => !x.IsDeleted)
            .HasIndex(x => x.IsDeleted)
            .HasFilter($"[{nameof(ToDoItem.IsDeleted)}] == 0");

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Title)
            .HasMaxLength(100);

        builder.HasIndex(x => x.DueDate);
        builder.HasIndex(x => new { x.IsCompleted, x.DueDate });
    }
}