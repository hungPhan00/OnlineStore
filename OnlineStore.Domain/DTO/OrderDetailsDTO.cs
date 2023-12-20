namespace OnlineStore.Domain.DTO
{
    public class OrderDetailsDTO
    {
        public int? Id { get; set; }
        public int? ClerkId { get; set; }

        public int? ProductId { get; set; }
        public float? UnitPrice { get; set; }
        public int? Quantity { get; set; }
    }
}
