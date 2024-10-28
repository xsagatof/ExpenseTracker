namespace SpendSmart.Models
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Linq;

	namespace SpendSmart.Models
	{
		public static class SeedData
		{
			public static void Initialize(IServiceProvider serviceProvider)
			{
				using (var context = new SpendSmartDbContext(
					       serviceProvider.GetRequiredService<
						       DbContextOptions<SpendSmartDbContext>>()))
				{
					// Look for any movies.
					if (context.Expenses.Any())
					{
						return;   // DB has been seeded
					}
					context.Expenses.AddRange(
						new Expense
						{
							Value = 100,
							Description = "Ticket"
						},
						new Expense
						{
							Value = 300,
							Description = "Products"
						},
						new Expense
						{
							Value = 50,
							Description = "ATTO card"
						}
					);
					context.SaveChanges();
				}
			}
		}
	}
}
