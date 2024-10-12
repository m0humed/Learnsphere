using E_Learning.Areas.Course.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Course.Controllers
{
	[Area("Course")]
	public class CourseController : Controller
	{
		private readonly ICourseFullDataViewModelService courseFullData;

		public CourseController(ICourseFullDataViewModelService courseFullData)
        {
			this.courseFullData = courseFullData;
		}

        public IActionResult Index(string id)
		{
			var data  = courseFullData.GetFullDataByIdAsync(id).Result;
			var numofLessons =  from s in data.Sections
							    select s.SectionLessons.Count;
			ViewBag.NumberOfLessons = numofLessons.Sum() ;


			return View(data);
		}

		public async Task AddtoCart(string CourseId , string UserId ) 
		{ 

		
		}
	}
}
