using System.Net.Sockets;

namespace AdminApplication.Models
{
    public class TravelPackageInOrder
    {
        public Guid Id { get; set; }

        public Guid TravelPackageId { get; set; }
        public TravelPackage? TravelPackage { get; set; }

        public Guid OrderId { get; set; }
        public Order? UserOrder { get; set; }
        public int NumberOfTravelers { get; set; }
    }
}
