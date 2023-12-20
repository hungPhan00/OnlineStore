namespace OnlineStore.cms.ViewModels
{
    public class OrdersViewModel
    {
        public int? Id { get; set; }

        public int? ClerkId { get; set; }
        public int? CustomerId { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
