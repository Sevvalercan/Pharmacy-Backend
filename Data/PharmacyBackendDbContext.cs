using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Backend.Models;

namespace Pharmacy_Backend.Data
{
    public class PharmacyBackendDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=SEVVAL;Database=Pharmacy;Trusted_Connection=True;TrustServerCertificate=True;",
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });

        }
        public DbSet<Ilac> Ilaclar { get; set; }
    }
}
