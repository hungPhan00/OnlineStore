namespace OnlineStore.cms.ViewModels
{
    public class OrderDetailsViewModel
    {
        public int? Id { get; set; }
        public int? ClerkId { get; set; }

        public int? ProductId { get; set; }
        public float? UnitPrice { get; set; }
        public int? Quantity { get; set; }
    }
}
