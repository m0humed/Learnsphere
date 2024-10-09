using System.ComponentModel.DataAnnotations;

namespace E_Learning.Areas.Authentication.Models
{
    public class RegisterRequest
    {
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The first name must contain only alphabetic characters.")]
        public string FName { get; set; }
       
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "The last name must contain only alphabetic characters.")]
        public string LName { get; set; }

        
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string RegisteredAs { get; set; }

    }
}
