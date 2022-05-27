using ApiProject.Lesson.Models;
using ApiProject.Lesson.Persistence.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using ApiProject.Lesson.Models.Communication;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiProject.Lesson.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly ILogger<UniversityController> _logger;
        private readonly IMapper _mapper;
        private DatabaseCxt _context;
        private IOptions<AppSettings> _setting;
        public UniversityController(ILogger<UniversityController> logger, 
            DatabaseCxt ctx

            )
        {
            _logger = logger;
            _context = ctx;
           // _setting = setting;
            //_mapper = mapper;
        }
       
        [HttpGet("Students")]
        public async Task<IActionResult> Get()
        {
              var students =  await _context.Studente.ToListAsync();
              return Ok(students);
        }

       
        [HttpGet("Corso/{Title}")]
        public async Task<IActionResult> GetByCorso(string Title)
        {
            Corso c = null;
            using (_context)
            {
                c = await  _context.Corso.Where(c => c.Name == Title).FirstAsync();
                var data = _context.Corso
               .Include(s => s.Students)
               .First(c => c.Id == c.Id);
                
                return Ok(data);
            }
           
        }

       /// <summary>
/// Deletes a specific TodoItem.
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
        [HttpGet("Students/{Name}")]
        public async Task<IActionResult> GetByStudente([FromServices] IOptions<AppSettings> setting,  string Name)
        {
            Studente s;
            using (_context)
            {
                try
                {
                    s = await  _context.Studente.Where(c => c.Name == Name).FirstAsync();
                    return Ok(s);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
          
        }

      
        [HttpPost]
        public IActionResult Post([FromBody] SaveStudenteResource value)
        {
            Studente result = null;
            try
            {
                try
                {
                     result = _context.Studente.Add(value.ToStudent()).Entity;
                    _context.SaveChanges();
                    return Ok(result);
                }
                catch (Exception)
                {

                    throw;
                }
               // var studente = _mapper.Map<SaveStudenteResource, Studente>(value);
                
            }
            catch (Exception ex)
            {
               throw;
            }
           
        }

       
        [HttpPut]
        public async Task<IActionResult> Put(int id,[FromBody] SaveStudenteResource payload)
        {
            //var studentePayload = _mapper.Map<SaveStudenteResource, Studente>(payload);
            var std = await _context.Studente.FindAsync(id);
            Studente stdRsrc = payload.ToStudent();
            std.Name = stdRsrc.Name;
            std.CorsoId = stdRsrc.CorsoId;  
            var result =  _context.Studente.Update(std).Entity;
            try
            {
                var res = await _context.SaveChangesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }          
           
        }

        
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            Studente s = await _context.Studente.FindAsync(id);
            _context.Studente.Remove(s);
            await _context.SaveChangesAsync();
        }
    }
}
