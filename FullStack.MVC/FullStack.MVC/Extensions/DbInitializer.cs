using FullStack.MVC.Data;
using FullStack.MVC.Data.Models;
using IdentityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FullStack.MVC.Extensions
{
    public static class DbInitializer
    {

        public static async Task<IApplicationBuilder> SeedDb(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                dbContext.Database.Migrate();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                await CreateRoles(roleManager);
                await CreateUsers(userManager);
            }

            return app;
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                var adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }

            bool operatorRoleExists = await roleManager.RoleExistsAsync("Operator");
            if (!operatorRoleExists)
            {
                var operatorRole = new IdentityRole("Operator");
                await roleManager.CreateAsync(operatorRole);
            }
        }

        private static async Task CreateUsers(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser adminUser = await userManager.FindByNameAsync("admin@fullstack.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin@fullstack.com",
                    Email = "admin@fullstack.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin12345_");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                
            }

            ApplicationUser operatorUser = await userManager.FindByNameAsync("operator@fullstack.com");
            if (operatorUser == null)
            {
                operatorUser = new ApplicationUser
                {
                    UserName = "operator@fullstack.com",
                    Email = "operator@fullstack.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(operatorUser, "Operator12345_");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(operatorUser, "Operator");
                }
            }
        }
    }
}
