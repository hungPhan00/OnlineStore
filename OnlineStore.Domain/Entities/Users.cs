using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Domain.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class Users
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        [Display(Name = "UserID")]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string CivilianId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public int Role { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}