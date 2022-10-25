using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Model;
using Services.IRepository;

namespace Services
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        private readonly UniversityDBContext _context;

        public GroupRepository(UniversityDBContext context) : base(context)
        {
            _context = context;
        }

        public int StudentCount(int groupId)
        {
            return (from s in _context.Students where s.GroupId == groupId select s).Count();
        }

        public IEnumerable<Group> TakeGroupInCourse(int courseId)
        {
            return from g in _context.Groups.Include(g => g.Course) where g.CourseId == courseId orderby g.GroupId select g;

        }

        public IEnumerable<Group> Include()
        {
            return _context.Groups.Include(g => g.Course).ToList();
        }
    }
}
