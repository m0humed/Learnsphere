using E_Learning.Models;
using E_Learning.Repositories.IReposatories;

namespace E_Learning.Repositories.Repository
{
    public class DatForInstructorRepository : IDataForInstructor
    {
        private readonly ApplicationDbContext context;

        public DatForInstructorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(DataForInstructor entity)
        {
            await context.DataForInstructors.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DataForInstructor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DataForInstructor> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(DataForInstructor New, DataForInstructor Old)
        {
            throw new NotImplementedException();
        }
    }
}
