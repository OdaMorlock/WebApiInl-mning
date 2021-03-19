using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi_InlämningAttempt4.Models;

#nullable disable

namespace WebApi_InlämningAttempt4.Data
{
    public partial class SqlDbContext : DbContext
    {
        public SqlDbContext()
        {
        }

        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<IssueUser> IssueUsers { get; set; }
        public virtual DbSet<SessionToken> SessionTokens { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public DbSet<SessionTokenCodeFirst> SessionTokenCodeFirsts { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Customer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditedDate).HasColumnType("datetime");

                entity.Property(e => e.IssueUserFirstName).HasMaxLength(50);

                entity.HasOne(d => d.IssueUser)
                    .WithMany(p => p.Issues)
                    .HasForeignKey(d => d.IssueUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Issues__IssueUse__286302EC");
            });

            modelBuilder.Entity<IssueUser>(entity =>
            {
                entity.Property(e => e.UserFirstName).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.IssueUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IssueUser__UserI__25869641");
            });

            modelBuilder.Entity<SessionToken>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AccessToken).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SessionTo__UserI__2A4B4B5E");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.PasswordSalt).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
