using System;
using System.Collections.Generic;

namespace ApiProject.Lesson.Models
{
    public class Corso
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DatePublished { get; set; }
        public virtual List<Studente>? Students { get; set; }
    }
}
