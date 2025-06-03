using Light_Shop.API.Models;

namespace Light_Shop.API.DTOs.Response
{
    public class UserResponse
    {
        public string? Id { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime BirthOfDate { get; set; }

        public ApplicationUserGender Gender { get; set; }

    }
}
