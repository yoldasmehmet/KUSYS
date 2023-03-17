using System;
using System.Collections.Generic;

namespace KUSYS.Common.Entities
{
    public partial class Student
    {
        public Student()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string? UserName { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
