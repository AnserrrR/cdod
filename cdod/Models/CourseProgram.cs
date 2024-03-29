﻿using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public class CourseProgram
    {
        [Key]
        public int Id { get; set; }

        public int Hours { get; set; }

        public string Name { get; set; }

        public string? ProgramFileUrl { get; set; }

        public string? Topics { get; set; }

        public IEnumerable<Course?> Courses { get; set; } = new List<Course?>();
    }
}
