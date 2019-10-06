using JetBrains.Annotations;
using LearningDDD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Infrastructure.Data.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            //optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseMySql("Server=132.232.77.146;Database=ddd;Uid=root;Pwd=123abc123;Character Set=utf8mb4;persist security info=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>().HasKey(s => s.Id);
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
                ar.Property(s => s.City).HasColumnType("varchar(50)");
                ar.Property(s => s.Province).HasColumnType("varchar(50)");
                ar.Property(s => s.StreetAndNumber).HasColumnType("varchar(100)");
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
