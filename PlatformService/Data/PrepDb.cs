using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProduction)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetRequiredService<AppDbContext>(), isProduction);
        }

        private static void SeedData(AppDbContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }

            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("--> Seeding Data...");
                context.Platforms.Add(new Platform { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" });
                context.Platforms.Add(new Platform { Name = "Angular", Publisher = "Google", Cost = "Free" });
                context.Platforms.Add(new Platform { Name = "React", Publisher = "Facebook", Cost = "Free" });
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
            context.SaveChanges();
        }
    }
}