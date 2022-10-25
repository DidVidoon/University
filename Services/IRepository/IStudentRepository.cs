using Model;

namespace Services.IRepository
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        IEnumerable<Student> TakeStudentInGroup(int groupId);

        IEnumerable<Student> Include();
    }
}
