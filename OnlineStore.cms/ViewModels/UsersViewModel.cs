namespace OnlineStore.cms.ViewModels
{
    public class UsersViewModel
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CivilianId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Role { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
