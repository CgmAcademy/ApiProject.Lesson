namespace Communication.Models
{
    public class Studente
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CorsoId { get; set; }
        public virtual Corso? Corso { get; set; }

    }
}
