using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    public class Categories
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string? Image { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
