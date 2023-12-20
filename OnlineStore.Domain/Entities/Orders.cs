using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class Orders
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        [Display(Name = "OrdersID")]
        public int Id { get; set; }

        public int? ClerkId { get; set; }
        public Users? ClerkUsers { get; set; }

        public int CustomerId { get; set; }
        public Users? CustomerUsers { get; set; }

        public DateTimeOffset CreateAt { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<OrderDetails>? OrderDetails { get; set; }

    }
}
