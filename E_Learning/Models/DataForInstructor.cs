using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class DataForInstructor
    {
        public string Id { get; set; }
        public string Profession { get; set; }
        public string Bio { get; set; }
        public int Balance { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
