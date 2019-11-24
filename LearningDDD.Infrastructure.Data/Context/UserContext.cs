using JetBrains.Annotations;
using LearningDDD.Domain.Core.Events;
using LearningDDD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningDDD.Infrastructure.Data.Context
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
            //optionsBuilder.UseMySql("Server=132.232.77.146;Database=ddd;Uid=root;Pwd=123abc123;Character Set=utf8mb4;persist security info=True;");
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
