﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyLibrary.Data;

#nullable disable

namespace MyLibrary.Migrations
{
    [DbContext(typeof(MyLibraryContext))]
    [Migration("20240731144713_update1")]
    partial class update1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyLibrary.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BookSetId")
                        .HasColumnType("int");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<int?>("LibraryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShelfId")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BookSetId");

                    b.HasIndex("LibraryId");

                    b.HasIndex("ShelfId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("MyLibrary.Models.BookSet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookSet");
                });

            modelBuilder.Entity("MyLibrary.Models.Library", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Library");
                });

            modelBuilder.Entity("MyLibrary.Models.Shelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.Property<double>("Space")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("LibraryId");

                    b.ToTable("Shelf");
                });

            modelBuilder.Entity("MyLibrary.Models.Book", b =>
                {
                    b.HasOne("MyLibrary.Models.BookSet", "BookSet")
                        .WithMany("Books")
                        .HasForeignKey("BookSetId");

                    b.HasOne("MyLibrary.Models.Library", "Library")
                        .WithMany()
                        .HasForeignKey("LibraryId");

                    b.HasOne("MyLibrary.Models.Shelf", "Shelf")
                        .WithMany("books")
                        .HasForeignKey("ShelfId");

                    b.Navigation("BookSet");

                    b.Navigation("Library");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("MyLibrary.Models.Shelf", b =>
                {
                    b.HasOne("MyLibrary.Models.Library", "Library")
                        .WithMany("shelves")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Library");
                });

            modelBuilder.Entity("MyLibrary.Models.BookSet", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MyLibrary.Models.Library", b =>
                {
                    b.Navigation("shelves");
                });

            modelBuilder.Entity("MyLibrary.Models.Shelf", b =>
                {
                    b.Navigation("books");
                });
#pragma warning restore 612, 618
        }
    }
}
