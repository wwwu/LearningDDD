﻿using LearningDDD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Infrastructure.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var config = new ConfigurationBuilder()
            //    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            ////optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>().HasKey(s => s.Id);
            modelBuilder.Entity<User>().Property(s => s.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(s => s.Password)
                .HasColumnType("varchar(100)")
                .IsRequired();
            modelBuilder.Entity<User>().Property(s => s.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();
            modelBuilder.Entity<User>().Property(s => s.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();
            //处理值对象配置，否则会被视为实体
            modelBuilder.Entity<User>().OwnsOne(s => s.Address, ar =>
            {
                ar.Property(s => s.City)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("Address_City");
                ar.Property(s => s.Province)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("Address_Province");
                ar.Property(s => s.StreetAndNumber)
                    .HasColumnType("varchar(100)")
                    .HasColumnName("Address_StreetAndNumber");
            });

            #endregion

            #region StoredEvent

            modelBuilder.Entity<StoredEvent>().HasKey(s => s.Id);
            modelBuilder.Entity<StoredEvent>().Property(s => s.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<StoredEvent>().Property(s => s.AggregateId)
                .HasColumnType("char(36)")
                .IsRequired();
            modelBuilder.Entity<StoredEvent>().Property(s => s.MessageType)
                .HasColumnType("varchar(200)");
            modelBuilder.Entity<StoredEvent>().Property(s => s.Data)
                .HasColumnType("json");
            modelBuilder.Entity<StoredEvent>().Property(s => s.User)
                .HasColumnType("varchar(50)");
            modelBuilder.Entity<StoredEvent>().Property(s => s.Timestamp)
                .HasColumnType("datetime(3)")
                .IsRequired();

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
