using Light_Shop.API.Models;
using System.ComponentModel.DataAnnotations;
using Light_Shop.API.Validations;

namespace Light_Shop.API.DTOs.Requests
{
    public class RegisterRequest
    {
        [MinLength(3)]
        public string FirstName { get; set; }
        [MinLength(3)]
        public string LastName { get; set; }

        [MinLength(6)]
        public string UserName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public ApplicationUserGender Gender { get; set; }
        

        [OverYears(15)]
        public DateTime BirthOfDate { get; set; }
        
    }
}
