using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class ProductImages
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public int Order { get; set; }
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public Products Products { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Path { get; set; }

    }
}
