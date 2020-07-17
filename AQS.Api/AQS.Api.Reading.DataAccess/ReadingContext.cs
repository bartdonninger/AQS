﻿using System;
using System.Linq;
using AQS.Api.Reading.DataAccess.Configurations;
using AQS.Api.Reading.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AQS.Api.Reading.DataAccess
{
    public class ReadingContext : DbContext
    {
        public ReadingContext(DbContextOptions<ReadingContext> options) : base (options) { }

        public DbSet<Domain.Models.Reading> Readings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReadingConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDateUtc = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDateUtc = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
