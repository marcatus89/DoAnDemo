using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DoAnTotNghiep.Data
{
    public static class SeedIdentity
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            string[] roleNames = { "Admin", "Sales", "Accounting", "Warehouse", "Logistics" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if(result.Succeeded)
                    {
                        logger.LogInformation($"Role '{roleName}' created successfully.");
                    }
                    else
                    {
                        logger.LogError($"Error creating role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            var adminEmail = "admin@mybathroom.com";
            var adminPassword = "Password123!";
            
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (createResult.Succeeded)
                {
                    logger.LogInformation("Admin user created successfully.");
                    var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if(roleResult.Succeeded)
                    {
                        logger.LogInformation("Admin role assigned to admin user successfully.");
                    }
                     else
                    {
                        logger.LogError($"Error assigning admin role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    logger.LogError($"Error creating admin user: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}

