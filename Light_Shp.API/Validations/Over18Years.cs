using System.ComponentModel.DataAnnotations;

namespace Light_Shop.API.Validations
{
    public class OverYears : ValidationAttribute
    {
        private int v;

        public OverYears(int v)
        {
            this.v = v;
        }

        public override bool IsValid(object? value)
        {
            if(value is DateTime date)
            {
                if (DateTime.Now.Year - date.Year >= v)
                    return true;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return $@"The field {name} must be at least {v} years or more.";
        }
    }
}


