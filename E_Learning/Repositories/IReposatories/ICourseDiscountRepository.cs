using E_Learning.Models;

namespace E_Learning.Repository.IReposatories
{
    public interface ICourseDiscountRepository : IRepository<CourseDiscount>
    {
        Task<IEnumerable<CourseDiscount>> GetDiscountsByCourseIdAsync(string courseId);
        Task<IEnumerable<CourseDiscount>> GetActiveDiscountsAsync();
    }

}
