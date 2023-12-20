using System.ComponentModel.DataAnnotations;
namespace OnlineStore.Domain.DTO
{
    public class ProductsDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public float UnitPrice { get; set; }
        public int CreatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreateAt { get; set; }
        public int CategoryId { get; set; }
        public bool IsDelete { get; set; }


    }
}
