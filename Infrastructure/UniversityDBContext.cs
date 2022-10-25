using Microsoft.EntityFrameworkCore;
using Model;

namespace Infrastructure
{
    public partial class UniversityDBContext : DbContext
    {
        public UniversityDBContext()
        {
        }

        public UniversityDBContext(DbContextOptions<UniversityDBContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=HOMEPC;Initial Catalog=Task9;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("Course_ID");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_Groups_Course_ID");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.CourseId).HasColumnName("Course_ID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CourseId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.GroupId, "IX_Students_Group_ID");

                entity.Property(e => e.StudentId).HasColumnName("Student_ID");

                entity.Property(e => e.First_Name).IsRequired();

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Last_Name).IsRequired();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GroupId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}