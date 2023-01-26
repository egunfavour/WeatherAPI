using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Weather_API.Domain.Models;

namespace Weather_API.Infrastructure
{
    public class Web_APIDbInitializer
    {
        public static async Task Seed(IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<Web_APIDbContext>();
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, @"Web_API.Infrastructure\Data\");
            if (await context.Database.EnsureCreatedAsync()) return;

            if (!context.Roles.Any())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var readText = await File.ReadAllTextAsync(filePath + "Roles.json");
                List<IdentityRole> Roles = JsonConvert.DeserializeObject<List<IdentityRole>>(readText);
                foreach (var role in Roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }
            if (!context.user.Any())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var readText = await File.ReadAllTextAsync(filePath + "Users.json");
                List<AppUser> users = JsonConvert.DeserializeObject<List<AppUser>>(readText);
                users.ForEach(delegate (AppUser user)
                {
                    userManager.CreateAsync(user, "Jaspino2_06$");
                    context.user.AddAsync(user);
                });
            }
            await context.SaveChangesAsync();

        }
    }
}