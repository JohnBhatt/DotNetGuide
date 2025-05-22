namespace DotNetGuide.Web.ViewModels
{
    public class UserIndexVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string Role { get; set; }
        public string? CompanyName { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
