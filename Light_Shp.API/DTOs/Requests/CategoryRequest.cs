using System.ComponentModel.DataAnnotations;

namespace Light_Shop.API.DTOs.Requests
{
    public class CategoryRequest
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
