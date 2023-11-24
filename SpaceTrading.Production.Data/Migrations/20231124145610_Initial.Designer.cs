﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpaceTrading.Production.Data;

#nullable disable

namespace SpaceTrading.Production.Data.Migrations
{
    [DbContext(typeof(SpaceTradingContext))]
    [Migration("20231124145610_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_CI_AS")
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SpaceTrading.Production.Data.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResourceCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceSizeId")
                        .HasColumnType("int");

                    b.Property<float>("UnitVolume")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("ResourceCategoryId");

                    b.HasIndex("ResourceSizeId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("SpaceTrading.Production.Data.Models.ResourceCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ResourcesCategories");
                });

            modelBuilder.Entity("SpaceTrading.Production.Data.Models.ResourceSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ResourceSizes");
                });

            modelBuilder.Entity("SpaceTrading.Production.Data.Models.Resource", b =>
                {
                    b.HasOne("SpaceTrading.Production.Data.Models.ResourceCategory", "ResourceCategory")
                        .WithMany("Resources")
                        .HasForeignKey("ResourceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SpaceTrading.Production.Data.Models.ResourceSize", "ResourceSize")
                        .WithMany("Resources")
                        .HasForeignKey("ResourceSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ResourceCategory");

                    b.Navigation("ResourceSize");
                });

            modelBuilder.Entity("SpaceTrading.Production.Data.Models.ResourceCategory", b =>
                {
                    b.Navigation("Resources");
                });

            modelBuilder.Entity("SpaceTrading.Production.Data.Models.ResourceSize", b =>
                {
                    b.Navigation("Resources");
                });
#pragma warning restore 612, 618
        }
    }
}
