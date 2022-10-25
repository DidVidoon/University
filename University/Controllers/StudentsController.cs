using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;
using Services.IRepository;

namespace Presentation.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private UniversityDBContext _DBContext;

        public StudentsController(IStudentRepository studentRepository, UniversityDBContext DBContext)
        {
            _studentRepository = studentRepository;
            _DBContext = DBContext;
        }

        public IActionResult Index(int groupId)
        {
            if (groupId == 0)
                return View(_studentRepository.Include());

            return View(_studentRepository.TakeStudentInGroup(groupId));
        }

        public IActionResult Create()
        {
            SelectList list = new(_DBContext.Groups, "GroupId", "Name");
            ViewBag.GroupId = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StudentId,GroupId,First_Name,Last_Name")] Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            _studentRepository.Add(student);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var student = _studentRepository.GetById((int)id);
            if (student == null)
                return NotFound();

            SelectList list = new(_DBContext.Groups, "GroupId", "Name");
            ViewBag.GroupName = list;

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StudentId,GroupId,First_Name,Last_Name")] Student student)
        {
            if (id != student.StudentId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(student);

            _studentRepository.Update(student);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var student = _studentRepository.GetById((int)id);
            if (student == null)
                return NotFound();

            student.Group = _DBContext.Groups.Where(c => c.GroupId == student.GroupId).First();

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
                return NotFound();

            _studentRepository.Delete(student);

            return RedirectToAction(nameof(Index));
        }
    }
}
