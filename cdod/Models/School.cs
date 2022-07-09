﻿using System.ComponentModel.DataAnnotations;

namespace cdod.Models
{
    public enum District
    {
        Central,
        Tractor
    }

    public class School
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public District District { get; set; }

        public IEnumerable<Student?> Students { get; set; } = new List<Student?>();
    }
}
