using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class Stocks
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        [Display(Name = "StocksID")]
        public int Id { get; set; }
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public Products ProductsStocks { get; set; }
        public int Quantity { get; set; }

        public ICollection<StockEvents> StockEvents { get; set; }
    }
}
