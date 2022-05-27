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
        

            Corso corso = new Corso()
                {
                    Name = "Informatica",
                    DatePublished = DateTime.Now,
                    Students = new List<Studente>()
                };
            List<Studente> students = new List<Studente>()
            {
                new Studente() { Name = "Bruno", DatePublished = DateTime.Now },
                new Studente() { Name = "Mario", DatePublished = DateTime.Now},
                new Studente() { Name = "Luca", DatePublished = DateTime.Now },
                new Studente() { Name = "Maria", DatePublished = DateTime.Now }
            };

            corso.Students.AddRange(students);

                dbCtx.Corso.Add(corso);

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
