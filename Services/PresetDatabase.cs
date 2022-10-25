using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace Services
{
    public class PresetDatabase
    {
        public static void Configurate(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<UniversityDBContext>(Options =>
                     Options.UseSqlServer(configuration.GetConnectionString("University"),
                     b => b.MigrationsAssembly(typeof(UniversityDBContext).Assembly.FullName)));
        }

        public static void Fill(IServiceProvider serviceProvider)
        {
            using (var context = new UniversityDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<UniversityDBContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Courses.Any())
                    return;

                context.Courses.AddRange(
                    new Course
                    {
                        Name = "Course1",
                        Description = "First course",
                    },

                    new Course
                    {
                        Name = "Course2",
                        Description = "Second course",
                    }
                );

                context.SaveChanges();

                if (context.Groups.Any())
                    return;


                context.Groups.AddRange(
                    new Group
                    {
                        CourseId = 1,
                        Name = "Group_1",
                    },

                    new Group
                    {
                        CourseId = 1,
                        Name = "Group_2",
                    },

                    new Group
                    {
                        CourseId = 2,
                        Name = "Group_3",
                    },

                    new Group
                    {
                        CourseId = 2,
                        Name = "Group_4",
                    }
                );

                if (context.Students.Any())
                    return;


                context.Students.AddRange(
                    new Student
                    {
                        GroupId = 1,
                        First_Name = "First Name",
                        Last_Name = "Last Name",
                    },

                    new Student
                    {
                        GroupId = 1,
                        First_Name = "First Name 1",
                        Last_Name = "Last Name 1",
                    },

                    new Student
                    {
                        GroupId = 2,
                        First_Name = "First Name 2",
                        Last_Name = "Last Name 2",
                    },

                    new Student
                    {
                        GroupId = 2,
                        First_Name = "First Name 3",
                        Last_Name = "Last Name 3",
                    },

                    new Student
                    {
                        GroupId = 3,
                        First_Name = "First Name 4",
                        Last_Name = "Last Name 4",
                    },

                    new Student
                    {
                        GroupId = 3,
                        First_Name = "First Name 5",
                        Last_Name = "Last Name 5",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
