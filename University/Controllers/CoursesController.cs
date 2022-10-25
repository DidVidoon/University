using Microsoft.AspNetCore.Mvc;
using Model;
using Services.IRepository;

namespace Presentation.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _coursesRepository;

        public CoursesController(ICourseRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public IActionResult Index()
        {
            return View(_coursesRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CourseId,Name,Description")] Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            _coursesRepository.Add(course);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var course = _coursesRepository.GetById((int)id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("CourseId,Name,Description")] Course course)
        {
            if (id != course.CourseId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(course);

            _coursesRepository.Update(course);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var course = _coursesRepository.GetById((int)id);
            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _coursesRepository.GetById(id);
            if (course == null)
                return NotFound();

            if (_coursesRepository.GroupsCount(course.CourseId) != 0)
            {
                ViewData["WarningMessage"] = "Cannot delete course with groups";
                return View(course);
            }

            _coursesRepository.Delete(course);
            return RedirectToAction(nameof(Index));
        }
    }
}
