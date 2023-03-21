using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TodoListTest.Models
{
    public partial class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivityModel> Activities { get; set; }
        public virtual DbSet<TodoModel> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityModel>(entity =>
            {
                entity.ToTable("activities");

                entity.Property(e => e.id).HasColumnName("activity_id");

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.title)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<TodoModel>(entity =>
            {
                entity.ToTable("todos");

                entity.Property(e => e.id).HasColumnName("todo_id");

                entity.Property(e => e.activity_group_id).HasColumnName("activity_group_id");

                entity.Property(e => e.created_at)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.priority)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("priority");

                entity.Property(e => e.title)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
