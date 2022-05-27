using System;
using System.ComponentModel.DataAnnotations;

namespace ApiProject.Lesson.Models.Communication
{
    public class SaveStudenteResource
    {
        // public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }       
        public  int CorsoId { get; set; }
        public Studente ToStudent() => new Studente()
        {
            Name = this.Name,
            CorsoId = this.CorsoId,

        };


    }
}
