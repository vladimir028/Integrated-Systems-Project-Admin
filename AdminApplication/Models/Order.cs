namespace AdminApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? EShopApplicationUserId { get; set; }
        public EShopApplicationUser? EShopApplicationUser { get; set; }
        public ICollection<TravelPackageInOrder>? TravelPackageInOrders { get; set; }

    }
}
