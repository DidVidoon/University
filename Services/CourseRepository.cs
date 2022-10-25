using Infrastructure;
using Model;
using Services.IRepository;

namespace Services
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly UniversityDBContext _context;

        public CourseRepository(UniversityDBContext context) : base(context)
        {
            _context = context;
        }

        public int GroupsCount(int courseId)
        {
            return _context.Groups.Where(g => g.CourseId == courseId).Count();
        }
    }
}