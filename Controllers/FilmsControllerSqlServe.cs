using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmManagerSqlServe_MongoDb.SqlServe.Context;
using FilmManagerSqlServe_MongoDb.SqlServe.EntitySqlServe;
using FilmManager.SqlServe.EntitySqlServe;

namespace FilmManagerSqlServe_MongoDb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmsControllerSqlServe : ControllerBase
    {
        private readonly FilmManagerContext _context;

        public FilmsControllerSqlServe(FilmManagerContext context)
        {
            _context = context;
        }

        // GET: api/FilmsControllerSqlServe
        [HttpGet("Index")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            return await _context.Films.Include(f=>f.FilmCategory).Include(f=>f.FilmsTagsPivots).ToListAsync();
        }

        // GET: api/FilmsControllerSqlServe/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<Film>> GetFilm(long id)
        {
            var film = await _context.Films.Include(f => f.FilmCategory).Include(f => f.FilmsTagsPivots).FirstOrDefaultAsync(f=>f.FilmId==id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }

        
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutFilm(long id,[FromBody]FilmDto filmDto)
        {
            var film = new Film()
            {
                FilmId = id,
                FilmName = filmDto.FilmName,
                FilmUrlImg = filmDto.FilmUrlImg,
                FilmDirector = filmDto.FilmDirector,
                FilmRelaseDate = filmDto.FilmRelaseDate,
                FilmCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == filmDto.FilmCategoryId),
                FilmsTagsPivots = new List<FilmsTagsPivot>()
            };


            if (id != film.FilmId)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FilmsControllerSqlServe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Film>> PostFilm([FromBody] FilmDto filmDto) 
        {
            var film = new Film()
            {
                FilmName = filmDto.FilmName,
                FilmUrlImg = filmDto.FilmUrlImg,
                FilmDirector = filmDto.FilmDirector,
                FilmRelaseDate = filmDto.FilmRelaseDate,
                FilmCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == filmDto.FilmCategoryId),
                FilmsTagsPivots = new List<FilmsTagsPivot>()
            };
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.FilmId }, film);
        }


        // DELETE: api/FilmsControllerSqlServe/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteFilm(long id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(long id)
        {
            return _context.Films.Any(e => e.FilmId == id);
        }
    }
}
