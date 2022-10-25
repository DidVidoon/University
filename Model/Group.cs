using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }

        public int GroupId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CourseId { get; set; }

        public virtual Course? Course { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
