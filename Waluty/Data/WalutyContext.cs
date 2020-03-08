using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Waluty.Models;

namespace Waluty.Data
{
    public class WalutyContext : DbContext
    {
        public WalutyContext (DbContextOptions<WalutyContext> options)
            : base(options)
        {
        }

        public DbSet<Waluty.Models.ExchangeRate> ExchangeRate { get; set; }

        public DbSet<Waluty.Models.Rate> Rate { get; set; }

    }
}
