using CrudOperationDotNetWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudOperationDotNetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext;
        public BrandController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        //GET Method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_dbcontext.Brands == null)
            {
                return NotFound();
            }
            return await _dbcontext.Brands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            if (_dbcontext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbcontext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }
        //POST

     [HttpPost]
      public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _dbcontext.Brands.Add(brand);
            await _dbcontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrand), new { id = brand.ID}, brand);
        }

     [HttpPut]
      public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.ID)
            {
                return BadRequest();
            }
            _dbcontext.Entry(brand).State = EntityState.Modified;
            try
            {
                await _dbcontext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool BrandAvailable(int id)
        {
            return (_dbcontext.Brands?.Any(x=>x.ID==id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrans(int id)
        {
            if (_dbcontext.Brands== null)

            {
                return NotFound();

            }
            var brand = await _dbcontext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _dbcontext.Brands.Remove(brand);
            await _dbcontext.SaveChangesAsync();
            return Ok();
        }



    }
}
