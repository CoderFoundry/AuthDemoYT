namespace AuthDemoYT.Models
{
    public class DashboardData
    {
        public int TotalUsers { get; set; }
        public int ActiveSessions { get; set; }
        public int NewSignups { get; set; }
        public int ErrorCount { get; set; }
        public List<Order> RecentOrders { get; set; } = new List<Order>();
    }
}
