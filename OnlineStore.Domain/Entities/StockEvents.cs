using OnlineStore.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class StockEvents
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        [Display(Name = "StockEventsID")]
        public int? Id { get; set; }

        [ForeignKey("Stocks")]
        public int? StockId { get; set; }

        public Stocks? Stocks { get; set; }

        public StockEventTypes? Type { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Reason { get; set; }

        public int? Quantity { get; set; }
        public DateTimeOffset? CreateAt { get; set; }
    }
}