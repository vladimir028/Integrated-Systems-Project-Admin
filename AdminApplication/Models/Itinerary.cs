namespace AdminApplication.Models
{
    public class Itinerary
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TravelPackage TravelPackage { get; set; }
        public Guid TravelPackageId { get; set; }
        public List<PlannedRoute> PlannedRoutes { get; set; }

        public Itinerary()
        {
            PlannedRoutes = new List<PlannedRoute>(getInitialSize());
        }

        public int getInitialSize()
        {
            return (EndDate.Date - StartDate.Date).Days;
        }
    }
}
