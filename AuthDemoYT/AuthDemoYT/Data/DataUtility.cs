using AuthDemoYT.Components.Account.Pages;
using AuthDemoYT.Data;
using AuthDemoYT.Models;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AuthDemoYT.Data
{
    public static class DataUtility
    {

        public static string? GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        public static string BuildConnectionString(string databaseUrl)
        {
            //Provides an object representation of a uniform resource identifier (URI) and easy access to the parts of the URI.
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            //Provides a simple way to create and manage the contents of connection strings used by the NpgsqlConnection class.
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
            };
            return builder.ToString();
        }

        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            //Service: An instance of RoleManager
            await using var dbContextSvc = svcProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext();
            //Service: An instance of RoleManager
            var roleManagerSvc = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //Service: An instance of the UserManager
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //Service: An IConfiguration instance to get appsettings/secrets/environment variables
            var configurationSvc = svcProvider.GetRequiredService<IConfiguration>();
            //Migration: This is the programmatic equivalent to Update-Database
            await dbContextSvc.Database.MigrateAsync();

            string defaultPassword = configurationSvc["DefaultPassword"] ??
                throw new ApplicationException("Error seeding data - no DefaultPassword configured!");

            await SeedRolesAsync(roleManagerSvc);
            await SeedDemoUsersAsync(userManagerSvc, defaultPassword);
            await SeedDefaultOrdersAsync(dbContextSvc);

            await dbContextSvc.DisposeAsync();
        }


        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            //Seed Role
            foreach (string roleName in Enum.GetNames<Role>())
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }


        public static async Task SeedDemoUsersAsync(UserManager<ApplicationUser> userManager, string defaultPassword)
        {
            //Seed Demo Admin User
            var defaultUser = new ApplicationUser
            {
                UserName = "demoadmin@rocketsaas.com",
                Email = "demoadmin@rocketsaas.com",
                FirstName = "Demo",
                LastName = "Admin",
                EmailConfirmed = true
            };
            try
            {
                //Test database to see if user already exists
                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, defaultPassword);
                    await userManager.AddToRoleAsync(defaultUser, nameof(Role.Admin));
                    await userManager.AddToRoleAsync(defaultUser, nameof(Role.AppUser));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Demo Admin User.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }


            //Seed Demo ProjectManager User
            defaultUser = new ApplicationUser
            {
                UserName = "demouser@rocketsaas.com",
                Email = "demouser@rocketsaas.com",
                FirstName = "Demo",
                LastName = "User",
                EmailConfirmed = true
            };
            try
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, defaultPassword);
                    await userManager.AddToRoleAsync(defaultUser, nameof(Role.AppUser));                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Demo ProjectManager1 User.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }

        }


        public static async Task SeedDefaultOrdersAsync(ApplicationDbContext context)
        {
            try
            {
                if (context.Orders.Any())
                {
                    // If orders already exist, skip seeding
                    return;
                }
                // Setup Faker for Order
                var orderFaker = new Faker<Order>()
                    .RuleFor(o => o.Id, f => f.IndexFaker + 1)
                    .RuleFor(o => o.CustomerName, f => f.Person.FullName)
                    .RuleFor(o => o.Date, f => f.Date.PastOffset(1).ToUniversalTime())
                    .RuleFor(o => o.Total, f => f.Finance.Amount(10, 500));

                // Generate 100 mock orders
                var orders = orderFaker.Generate(100);
                context.Orders.AddRange(orders);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("*************  ERROR  *************");
                Console.WriteLine("Error Seeding Orders.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
                throw;
            }
        }

    }

}
