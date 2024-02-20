using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class Products
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        [Display(Name = "ProductID")]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Thumbnail { get; set; }

        public float UnitPrice { get; set; }

        [ForeignKey("Users")]
        public int? CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateAt { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        public bool IsDelete { get; set; }

        public Users? Users { get; set; }
        public Stocks? Stock { get; set; }
        public Categories Categories { get; set; }
        public ICollection<ProductImages>? ProductImages { get; set; }

        public ICollection<OrderDetails>? OrderDetails { get; set; }

        public override string ToString()
        {
            return "id" + Id + "name" + Name + "desc" + Description +
                "price" + UnitPrice + "createBy" + CreatedBy +
                "category" + CategoryId +
                "delete" + IsDelete;
        }
    }
}