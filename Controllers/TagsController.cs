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
    public class TagsController : ControllerBase
    {
        private readonly FilmManagerContext _context;

        public TagsController(FilmManagerContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet("Index")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<Tag>> GetTag(long id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

       

        private bool TagExists(long id)
        {
            return _context.Tags.Any(e => e.TagId == id);
        }
    }
}
