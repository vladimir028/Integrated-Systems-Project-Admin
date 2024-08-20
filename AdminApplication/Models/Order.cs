namespace AdminApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? OwnerId { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public ICollection<TravelPackageInOrder>? TravelPackageInOrders { get; set; }

    }
}
