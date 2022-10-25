using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Model;
using Services.IRepository;

namespace Services
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly UniversityDBContext _context;

        public StudentRepository(UniversityDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Student> TakeStudentInGroup(int groupId)
        {
            return from s in _context.Students.Include(s => s.Group) where (s.GroupId == groupId) orderby s.StudentId select s;
        }

        public IEnumerable<Student> Include()
        {
            return _context.Students.Include(s => s.Group).ToList();
        }
    }
}
