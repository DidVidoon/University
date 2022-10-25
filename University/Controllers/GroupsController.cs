using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Services.IRepository;

namespace Presentation.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupRepository _groupsRepository;
        private readonly UniversityDBContext _DBContext;

        public GroupsController(IGroupRepository groupsRepository, UniversityDBContext context)
        {
            _groupsRepository = groupsRepository;
            _DBContext = context;
        }

        public IActionResult Index(int courseId)
        {
            if (courseId == 0)
                return View(_groupsRepository.Include());

            return View(_groupsRepository.TakeGroupInCourse(courseId));
        }

        public IActionResult Create()
        {
            SelectList list = new(_DBContext.Courses, "CourseId", "Name");
            ViewBag.CourseId = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("GroupId,CourseId,Name")] Group group)
        {
            if (!ModelState.IsValid)
                return View(group);

            _groupsRepository.Add(group);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var group = _groupsRepository.GetById((int)id);
            if (group == null)
                return NotFound();

            SelectList list = new(_DBContext.Courses, "CourseId", "Name");
            ViewBag.CourseId = list;

            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("GroupId,CourseId,Name")] Group group)
        {
            if (id != group.GroupId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(group);

            _groupsRepository.Update(group);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var group = _groupsRepository.GetById((int)id);
            if (group == null)
                return NotFound();

            group.Course = _DBContext.Courses.Where(c => c.CourseId == group.CourseId).First();
            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var group = _groupsRepository.GetById(id);
            if (group == null)
                return NotFound();

            if (_groupsRepository.StudentCount(group.GroupId) != 0)
            {
                ViewData["WarningMessage"] = "Cannot delete group with students";
                return View(group);
            }

            _groupsRepository.Delete(group);
            return RedirectToAction(nameof(Index));

        }
    }
}
