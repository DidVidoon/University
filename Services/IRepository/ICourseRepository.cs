using Model;

namespace Services.IRepository
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        int GroupsCount(int courseId);
    }
}
