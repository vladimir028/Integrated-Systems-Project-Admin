using System.Diagnostics;

namespace AdminApplication.Models
{
    public class PlannedRoute
    {
        public Guid Id { get; set; }
        public string RouteDescription { get; set; }
        public List<Activity> Activities { get; set; }
        public Itinerary Itinerary { get; set; }
        public Guid ItineraryId { get; set; }
        public PlannedRoute()
        {
            this.Activities = new List<Activity>();
        }
    }
}
