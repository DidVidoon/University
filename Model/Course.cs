using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Course
    {
        public Course()
        {
            Groups = new HashSet<Group>();
        }

        public int CourseId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}