namespace OnlineStore.Domain.DTO
{
    public class CategoriesDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
    }
}
