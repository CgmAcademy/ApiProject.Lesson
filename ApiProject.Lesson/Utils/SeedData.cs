using ApiProject.Lesson.Persistence.Configuration;
using System;
using System.Collections.Generic;
using ApiProject.Lesson.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiProject.Lesson.Utils
{
    public static class SeedData
    {
        public static async Task SeedDatabase(DatabaseCxt dbCtx)
        {           
          
                Clear(dbCtx.Studente);
        

            Corso Informatica = new Corso()
                {
                    Name = "Informatica",
                    DatePublished = DateTime.Now,
                    Students = new List<Studente>()
                };
            Corso Lettere = new Corso()
            {
                Name = "Lettere",
                DatePublished = DateTime.Now,
                Students = null
            };
            List<Studente> students = new List<Studente>()
            {
                new Studente() { Name = "Bruno", DatePublished = DateTime.Now },
                new Studente() { Name = "Mario", DatePublished = DateTime.Now},
                new Studente() { Name = "Luca", DatePublished = DateTime.Now },
                new Studente() { Name = "Maria", DatePublished = DateTime.Now }
            };

            Informatica.Students.AddRange(students);
            
            dbCtx.Corso.Add(Informatica);
            dbCtx.Corso.Add(Lettere);

            try
                {
                    await  dbCtx.SaveChangesAsync();                    
                }
                catch (Exception ex)
                {

                    throw;
                }
            
        }
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            if (dbSet.Any())
            {
                dbSet.RemoveRange(dbSet.ToList());
            }
        }
    }
}
