﻿// <auto-generated />
using System;
using API.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("site.Models.Anime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("AiredOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Episodes")
                        .HasColumnType("int");

                    b.Property<int>("EpisodesAired")
                        .HasColumnType("int");

                    b.Property<string>("ImageOriginal")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ImagePreview")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ImageX48")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ImageX96")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("ReleasedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Russian")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("anime", t =>
                        {
                            t.HasCheckConstraint("Score", "Score < 10");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
