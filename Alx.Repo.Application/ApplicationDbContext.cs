using Alx.Repo.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alx.Repo.Application
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasKey(p => p.Id);

            //modelBuilder.Entity<Item>().HasData(
            //    new Item { Id = 1, Name = "Item 1", UserId = "UserId", ParentId = 0, Domain = "Domain", Content = "Content", Description = "Descriptrion", AuditCreatedByUser = "AuditCreatedByUser", AuditLastUpdatedByUser = "AuditLastUpdatedByUser", AuditCreatedOn  = DateTime.Now, AuditLastUpdated  = DateTime.Now },
            //    new Item { Id = 2, Name = "Item 2", UserId = "UserId", ParentId = 0, Domain = "Domain", Content = "Content", Description = "Descriptrion", AuditCreatedByUser = "AuditCreatedByUser", AuditLastUpdatedByUser = "AuditLastUpdatedByUser", AuditCreatedOn = DateTime.Now, AuditLastUpdated = DateTime.Now },
            //    new Item { Id = 3, Name = "Item 3", UserId = "UserId", ParentId = 0, Domain = "Domain", Content = "Content", Description = "Descriptrion", AuditCreatedByUser = "AuditCreatedByUser", AuditLastUpdatedByUser = "AuditLastUpdatedByUser", AuditCreatedOn = DateTime.Now, AuditLastUpdated = DateTime.Now }
            //);
        }
    }
}
