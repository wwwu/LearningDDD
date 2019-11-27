﻿// <auto-generated />
using System;
using LearningDDD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LearningDDD.Infrastructure.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20191006065456_UpdateUser")]
    partial class UpdateUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LearningDDD.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LearningDDD.Domain.Models.User", b =>
                {
                    b.OwnsOne("LearningDDD.Domain.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId");

                            b1.Property<string>("City")
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Province")
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("StreetAndNumber")
                                .HasColumnType("varchar(100)");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.HasOne("LearningDDD.Domain.Models.User")
                                .WithOne("Address")
                                .HasForeignKey("LearningDDD.Domain.Models.Address", "UserId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}