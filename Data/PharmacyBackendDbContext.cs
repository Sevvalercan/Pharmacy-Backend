using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Backend.Models;

namespace Pharmacy_Backend.Data
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }

        public DbSet<Ilac> Ilaclar { get; set; }
    }
}
