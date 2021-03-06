// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi_InlämningAttempt4.Data;

namespace WebApi_InlämningAttempt4.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    partial class SqlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.Issue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ActivityStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("EditedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("FinishedStatus")
                        .HasColumnType("bit");

                    b.Property<string>("IssueUserFirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("IssueUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IssueUserId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.IssueUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserFirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("IssueUsers");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.SessionToken", b =>
                {
                    b.Property<byte[]>("AccessToken")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasIndex("UserId");

                    b.ToTable("SessionTokens");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.SessionTokenCodeFirst", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("AccessToken")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserId");

                    b.ToTable("SessionTokenCodeFirsts");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.Issue", b =>
                {
                    b.HasOne("WebApi_InlämningAttempt4.Models.IssueUser", "IssueUser")
                        .WithMany("Issues")
                        .HasForeignKey("IssueUserId")
                        .HasConstraintName("FK__Issues__IssueUse__286302EC")
                        .IsRequired();

                    b.Navigation("IssueUser");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.IssueUser", b =>
                {
                    b.HasOne("WebApi_InlämningAttempt4.Models.User", "User")
                        .WithMany("IssueUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__IssueUser__UserI__25869641")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.SessionToken", b =>
                {
                    b.HasOne("WebApi_InlämningAttempt4.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__SessionTo__UserI__2A4B4B5E")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.IssueUser", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("WebApi_InlämningAttempt4.Models.User", b =>
                {
                    b.Navigation("IssueUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
