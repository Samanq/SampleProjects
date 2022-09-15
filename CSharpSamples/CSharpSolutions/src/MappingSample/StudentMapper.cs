using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingSample
{
    public static class StudentMapper
    {
        /// <summary>
        /// Gets a list of dynamic objects and returns a list of students.
        /// </summary>
        /// <param name="list">list of dynamic objects.</param>
        /// <returns>The Converted List of students.</returns>
        public static IEnumerable<Student> MapToStudent(IEnumerable<dynamic> list)
        {
            var students = list.Select(t =>
            new Student
            {
                FirstName = t.first_name,
                LastName = t.last_name,
                Age = t.person_age
            });

            return students;
        }
    }
}
