using E_Learning.Models;
namespace E_Learning.Repository.IReposatories
{
    public interface ICoursePreviewRepository : IRepository<CoursePreview>
    {
        Task<IEnumerable<CoursePreview>> GetPreviewsByCourseIdAsync(string courseId);
    }

}
