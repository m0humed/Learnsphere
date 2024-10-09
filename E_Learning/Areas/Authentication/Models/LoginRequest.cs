using System.ComponentModel.DataAnnotations;

namespace E_Learning.Areas.Authentication.Models
{
    public class LoginRequest
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
