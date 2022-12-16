using Communication.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientHttp.Lesson
{

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowStudente(Studente product)
        {
            Console.WriteLine($"Name:{product.Name}\tCorsoId: " +
                $"{product.CorsoId}");
        }

        static async Task<Uri> CreateStudenteAsync(SaveStudenteResource product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/University/", product);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Studente> GetStudentByNameAsync(string Name)
        {
            Studente std = null;
            HttpResponseMessage response = await client.GetAsync($"api/University/Students/{Name}");
            if (response.IsSuccessStatusCode)
            {
                std = await response.Content.ReadAsAsync<Studente>();
            }
            return std;
        }

        static async Task<Studente> UpdateStudenteAsync(int id, SaveStudenteResource studenteRsc)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"/api/University/Student/{id}", studenteRsc);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            var studente = await response.Content.ReadAsAsync<Studente>();
            return studente;
        }

        static async Task<HttpStatusCode> DeleteStudenteAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/University/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
            Console.WriteLine("this is the end!"); 
            Console.ReadLine();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new studente
                SaveStudenteResource studenteRsc = new SaveStudenteResource
                {
                    Name = "Marco"
                };

                var url = await CreateStudenteAsync(studenteRsc);
                Console.WriteLine($"Created at {url}");

                // Get the studente
                Studente studente = await GetStudentByNameAsync(studenteRsc.Name);
                ShowStudente(studente);

                // Update the studente
                Console.WriteLine("Updating Studente...");
                studente.Name = "Anna";
                Studente std = await UpdateStudenteAsync(studente.Id, new SaveStudenteResource() { Name = studente.Name });

                // Get the updated studente
                studente = await GetStudentByNameAsync(std.Name);
                ShowStudente(studente);

                // Delete the studente
                var statusCode = await DeleteStudenteAsync(studente.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
