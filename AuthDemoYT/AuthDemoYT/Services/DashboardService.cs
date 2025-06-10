using AuthDemoYT.Models;
using AuthDemoYT.Services.Interfaces;
using Bogus;

namespace AuthDemoYT.Services
{
    public class DashboardService : IDashboardService
    {
        public Task<DashboardData> GetDashboardDataAsync()
        {
            // Setup Faker for Order
            var orderFaker = new Faker<Order>()
                .RuleFor(o => o.Id, f => f.IndexFaker + 1)
                .RuleFor(o => o.CustomerName, f => f.Person.FullName)
                .RuleFor(o => o.Date, f => f.Date.Past(1))
                .RuleFor(o => o.Total, f => f.Finance.Amount(10, 500));

            // Generate 500 mock orders
            var orders = orderFaker.Generate(10);

            // Build dashboard data
            var data = new DashboardData
            {
                TotalUsers = 1250,
                ActiveSessions = 230,
                NewSignups = 15,
                ErrorCount = 3,
                RecentOrders = orders
            };

            return Task.FromResult(data);
        }
    }

}
