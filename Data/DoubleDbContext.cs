using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Data
{
    public class DoubleDbContext(DbContextOptions<SpendSmartContext> options)
    {
        public DbSet<SpendSmart.Models.DoubleViewModel> Expense { get; set; } = default!;
    }
}
