using Model;

namespace Services.IRepository
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        int StudentCount(int groupId);

        IEnumerable<Group> TakeGroupInCourse(int courseId);

        IEnumerable<Group> Include();
    }
}
