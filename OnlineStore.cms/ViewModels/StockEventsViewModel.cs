namespace OnlineStore.cms.ViewModels
{
    public class StockEventsViewModel
    {
        public int? Id { get; set; }
        public int? StockId { get; set; }
        public int? Type { get; set; }
        public string? Reason { get; set; }
        public int? Quantity { get; set; }
        public DateTimeOffset? CreateAt { get; set; }
    }
}
