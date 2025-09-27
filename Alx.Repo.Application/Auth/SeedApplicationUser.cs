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

            var role1 = config.GetSection("Roles:Role1").Value ?? throw new InvalidOperationException("Config[Roles:Role1] not found.");
            var role2 = config.GetSection("Roles:Role2").Value ?? throw new InvalidOperationException("Config[Roles:Role2] not found.");


            // Ensure database is created and migrations applied
            context.Database.Migrate();

            // Create default roles if they don't exist
            if (!await roleManager.RoleExistsAsync(role1))
            {
                await roleManager.CreateAsync(new IdentityRole(role1));
            }
            if (!await roleManager.RoleExistsAsync(role2))
            {
                await roleManager.CreateAsync(new IdentityRole(role2));
            }

            var email = config.GetSection("Users:Primary:Email").Value ?? throw new InvalidOperationException("Config[Users:Primary:Email] not found.");
            var firstName = config.GetSection("Users:Primary:FirstName").Value ?? throw new InvalidOperationException("Config[Users:Primary:FirstName] not found.");
            var lastName = config.GetSection("Users:Primary:LastName").Value ?? throw new InvalidOperationException("Config[Users:Primary:LastName] not found.");
            var appDomain = config.GetSection("Users:Primary:AppDomain").Value ?? throw new InvalidOperationException("Config[Users:Primary:AppDomain] not found.");
            var password = config.GetSection("Users:Primary:Password").Value ?? throw new InvalidOperationException("Config[Users:Primary:Password] not found.");

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    Email = email,
                    AppDomain = appDomain,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newUser, password); 
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, role1);
                }
            }

            email = config.GetSection("Users:Secondary:Email").Value ?? throw new InvalidOperationException("Config[Users:Secondary:Email] not found.");
            firstName = config.GetSection("Users:Secondary:FirstName").Value ?? throw new InvalidOperationException("Config[Users:Secondary:FirstName] not found.");
            lastName = config.GetSection("Users:Secondary:LastName").Value ?? throw new InvalidOperationException("Config[Users:Secondary:LastName] not found.");
            appDomain = config.GetSection("Users:Secondary:AppDomain").Value ?? throw new InvalidOperationException("Config[Users:Secondary:AppDomain] not found.");
            password = config.GetSection("Users:Secondary:Password").Value ?? throw new InvalidOperationException("Config[Users:Secondary:Password] not found.");

            user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserName = email,
                    Email = email,
                    AppDomain = appDomain,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, role2);
                }
            }
        }
    }
}