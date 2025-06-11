using AuthDemoYT.Data;
using AuthDemoYT.Models;
using AuthDemoYT.Services.Interfaces;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace AuthDemoYT.Services
{
    public class DashboardService(IDbContextFactory<ApplicationDbContext> contextFactory) : IDashboardService
    {
        public async Task<DashboardData> GetDashboardDataAsync()
        {
            await using var dbContext = contextFactory.CreateDbContext();
            var orders = await dbContext.Orders
                .OrderByDescending(o => o.Date)
                .Take(10)
                .ToListAsync();

            // Build dashboard data
            var data = new DashboardData
            {
                TotalUsers = 1250,
                ActiveSessions = 230,
                NewSignups = 15,
                ErrorCount = 3,
                RecentOrders = orders
            };

            return data;
        }
    }

}
