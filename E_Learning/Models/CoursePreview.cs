using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{    
   public class CoursePreview
   {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public  string Title { get; set; }
		[Url(ErrorMessage = "this is not valid URL")]
		public List<string> Videourl { get; set; }
        public string CourseId { get; set; }
   }
    
}
