using DocumentFormat.OpenXml.InkML;
using System.ComponentModel.DataAnnotations;

namespace AdminApplication.Models
{
    public class TravelPackage
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public List<String>? Images { get; set; }
        public string? ImageTextBox { get; set; }
        public virtual Agency? Agency { get; set; }
        public Guid AgencyId { get; set; }
        public bool AlreadyhasItinerary { get; set; } = false;
        public Itinerary Itinerary { get; set; }
    }
}
