using Alx.Repo.Application;
using Alx.Repo.Application.Auth;
using Alx.Repo.Application.Auth.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;


public static class SeedApplicationUser
{
    public static async Task Initialize(IServiceProvider serviceProvider, IConfigurationManager config)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

            // Ensure database is created and migrations applied
            context.Database.Migrate();

            // Create default roles if they don't exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var email = config.GetSection("DefaultUser:Email").Value ?? throw new InvalidOperationException("Config[DefaultUser:Email] not found.");
            var firstName = config.GetSection("DefaultUser:FirstName").Value ?? throw new InvalidOperationException("Config[DefaultUser:FirstName] not found.");
            var lastName = config.GetSection("DefaultUser:LastName").Value ?? throw new InvalidOperationException("Config[DefaultUser:LastName] not found.");
            var appDomain = config.GetSection("DefaultUser:AppDomain").Value ?? throw new InvalidOperationException("Config[DefaultUser:AppDomain] not found.");
            var password = config.GetSection("DefaultUser:Password").Value ?? throw new InvalidOperationException("Config[DefaultUser:Password] not found.");

            var adminUser = await userManager.FindByEmailAsync(email);
            if (adminUser == null)
            {
                var newAdminUser = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    Email = email,
                    AppDomain = appDomain,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newAdminUser, password); // Replace with a strong password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
            }
        }
    }
}