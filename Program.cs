using Microsoft.EntityFrameworkCore;
using SpendSmart.Models;
using Microsoft.Extensions.DependencyInjection;
using SpendSmart.Data;
using SpendSmart.Models;
using SpendSmart.Models.SpendSmart.Models;

namespace SpendSmart
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddDbContext<SpendSmartContext>(options =>
			    options.UseSqlServer(builder.Configuration.GetConnectionString("SpendSmartContext") ?? throw new InvalidOperationException("Connection string 'SpendSmartContext' not found.")));

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<SpendSmartDbContext>(options =>
				options.UseInMemoryDatabase("SpendSmartDb")
				);

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				SeedData.Initialize(services);
			}

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
