using System;
using System.Collections.Generic;

namespace KUSYS.Common.Entities
{
    public partial class Course
    {
        public Course()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
