using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class OrderDetails
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        [Display(Name = "OrderDetailsID")]
        public int Id { get; set; }

        [ForeignKey("Orders ")]
        public int ClerkId { get; set; }
        public Orders Orders { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public Products Products { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
