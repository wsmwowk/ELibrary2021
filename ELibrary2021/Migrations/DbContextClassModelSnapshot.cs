// <auto-generated />
using System;
using ELibrary2021.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ELibrary2021.Migrations
{
    [DbContext(typeof(DbContextClass))]
    partial class DbContextClassModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ELibrary2021.DataModel.BooksModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookAuthor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookLang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BookParts")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ELibrary2021.DataModel.LogModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("OperationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("operation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("ELibrary2021.DataModel.ReturnedBooks", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("date");

                    b.Property<int>("period")
                        .HasColumnType("int");

                    b.Property<string>("takerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Returner");
                });

            modelBuilder.Entity("ELibrary2021.DataModel.TakerModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BookClass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BookIdid")
                        .HasColumnType("int");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TakedDate")
                        .HasColumnType("date");

                    b.Property<string>("TakerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TakerPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("periodInDays")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("BookIdid");

                    b.ToTable("Takers");
                });

            modelBuilder.Entity("ELibrary2021.DataModel.UsersModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ELibrary2021.DataModel.TakerModel", b =>
                {
                    b.HasOne("ELibrary2021.DataModel.BooksModel", "BookId")
                        .WithMany()
                        .HasForeignKey("BookIdid");

                    b.Navigation("BookId");
                });
#pragma warning restore 612, 618
        }
    }
}
