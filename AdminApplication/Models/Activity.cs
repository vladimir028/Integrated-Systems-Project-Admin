namespace AdminApplication.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public string ThingsToDo { get; set; }
        public PlannedRoute PlannedRoute { get; set; }
        public Guid PlannedRouteId { get; set; }
    }
}
