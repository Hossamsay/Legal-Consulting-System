using LegalConsulting.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace LegalConsulting.DAL
{
    public class ConsultingContext : DbContext
    {
        public ConsultingContext() : base("ConsultingContext")
        { }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Case> Cases { get; set; }

        public DbSet<OfficeLocation> officeLocations { get; set; }
        public DbSet<CaseDetail> CaseDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Case>()
            .HasMany(c => c.Lawyers).WithMany(i => i.Cases)
            .Map(t => t.MapLeftKey("CaseID")
            .MapRightKey("LawyerID")
            .ToTable("CaseAdminstrator"));

            modelBuilder.Entity<Case>()
            .Property(p => p.RowVersion).IsConcurrencyToken();


        }

    }
}