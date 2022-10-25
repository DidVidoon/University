using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        public string? First_Name { get; set; }

        [Required]
        public string? Last_Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int GroupId { get; set; }

        public virtual Group? Group { get; set; }
    }
}
