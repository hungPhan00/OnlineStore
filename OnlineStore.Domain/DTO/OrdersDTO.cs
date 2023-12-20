namespace OnlineStore.Domain.DTO
{
    public class OrdersDTO
    {
        public int? Id { get; set; }

        public int? ClerkId { get; set; }
        public int? CustomerId { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
