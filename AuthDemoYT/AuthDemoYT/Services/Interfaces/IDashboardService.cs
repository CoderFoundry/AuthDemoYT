using AuthDemoYT.Models;

namespace AuthDemoYT.Services.Interfaces
{
    public interface IDashboardService
    {
       Task<DashboardData> GetDashboardDataAsync();
    }
}
