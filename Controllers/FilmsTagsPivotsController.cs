using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmManagerSqlServe_MongoDb.SqlServe.Context;
using FilmManagerSqlServe_MongoDb.SqlServe.EntitySqlServe;

namespace FilmManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FilmsTagsPivotsController : ControllerBase
    {
        private readonly FilmManagerContext _context;

        public FilmsTagsPivotsController(FilmManagerContext context)
        {
            _context = context;
        }

        
       

        // POST: api/FilmsTagsPivots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<FilmsTagsPivot>> PostFilmsTagsPivot(FilmsTagsPivot filmsTagsPivot)
        {
            _context.FilmsTagsPivots.Add(filmsTagsPivot);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FilmsTagsPivotExists(filmsTagsPivot.FilmTagId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFilmsTagsPivot", new { id = filmsTagsPivot.FilmTagId }, filmsTagsPivot);
        }


        private bool FilmsTagsPivotExists(long id)
        {
            return _context.FilmsTagsPivots.Any(e => e.FilmTagId == id);
        }
    }
}
