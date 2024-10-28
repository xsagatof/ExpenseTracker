using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpendSmart.Models;

namespace SpendSmart.Data
{
    public class SpendSmartContext : DbContext
    {
        public SpendSmartContext(DbContextOptions<SpendSmartContext> options)
            : base(options)
        {
        }

        public DbSet<SpendSmart.Models.Expense> Expense { get; set; } = default!;
    }
}
