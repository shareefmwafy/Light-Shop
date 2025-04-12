using System.ComponentModel.DataAnnotations;

namespace Light_Shop.API.Validations
{
    public class Over18Years : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is not DateTime date) return false;
            return DateTime.Now.Year - date.Year >= 18;
        }

        public override string FormatErrorMessage(string name)
        {
            return $@"The field {name} must be at least 18 years or more.";
        }
    }
}


