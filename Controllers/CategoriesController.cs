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
    public class CategoriesController : ControllerBase
    {
        private readonly FilmManagerContext _context;

        public CategoriesController(FilmManagerContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet("Index")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<Category>> GetCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        

        private bool CategoryExists(long id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
