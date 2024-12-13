﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Product.Persistence.Models;

public static class IdentitySeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Define roles to seed
        var roles = new[] { "Admin", "User"};

        // Seed roles
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new ApplicationRole(role));
            }
        }

        // Define the admin user details
        var adminEmail = "admin@gmail.com";
        var adminPassword = "Admin@123";

        // Check if the admin user already exists
       var userExist = await userManager.FindByEmailAsync(adminEmail);
        if (userExist == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,     
                FullName = "Admin",
                PhoneNumber="0712345678",
                EmailConfirmed = true
            };

            // Create the admin user
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                // Assign the Admin role to the user
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            else
            {
                throw new Exception("Failed to create the admin user: " + string.Join(", ", result.Errors));
            }
        }
    }
}