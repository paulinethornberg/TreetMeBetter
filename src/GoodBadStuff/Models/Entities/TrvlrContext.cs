﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GoodBadStuff.Models.Entities
{
    public partial class TrvlrContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=trvlr.database.windows.net;Initial Catalog=TRVLRdb;Persist Security Info=True;User ID=trvlr;Password=Secret123");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TravelInfo>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.FromAddress).HasColumnType("varchar(40)");

                entity.Property(e => e.ToAddress).HasColumnType("varchar(40)");

                entity.Property(e => e.Transport).HasColumnType("varchar(30)");

                entity.Property(e => e.UserId).HasMaxLength(450);
            });
        }

        public virtual DbSet<TravelInfo> TravelInfo { get; set; }
    }
}