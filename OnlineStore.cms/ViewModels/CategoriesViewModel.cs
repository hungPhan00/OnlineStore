using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.cms.ViewModels
{
    public class CategoriesViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
    }
}
