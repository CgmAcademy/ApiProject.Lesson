using System;

namespace ApiProject.Lesson.Models
{
    public class Studente
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CorsoId { get; set; }
        public virtual Corso? Corso { get; set; }
        public Studente ToResource() => new Studente()
        {
            Name = this.Name,
            CorsoId = this.CorsoId,

        };
    }
}
