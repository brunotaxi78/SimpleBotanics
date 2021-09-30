using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassLib.Data;
using ClassLib.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSuppliersController : ControllerBase
    {
        private readonly ClassLib.Data.AppContext _context;

        public ProductSuppliersController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }

        // GET: api/ProductSuppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSupplier>>> GetProductsSuppliers()
        {
            return await _context.ProductsSuppliers.ToListAsync();
        }

        // GET: api/ProductSuppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSupplier>> GetProductSupplier(int id)
        {
            var productSupplier = await _context.ProductsSuppliers.FindAsync(id);

            if (productSupplier == null)
            {
                return NotFound();
            }

            return productSupplier;
        }

        // PUT: api/ProductSuppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductSupplier(int id, ProductSupplier productSupplier)
        {
            if (id != productSupplier.ProductSupplierID)
            {
                return BadRequest();
            }

            _context.Entry(productSupplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSupplierExists(id))
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

        // POST: api/ProductSuppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductSupplier>> PostProductSupplier(ProductSupplier productSupplier)
        {
            _context.ProductsSuppliers.Add(productSupplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductSupplier", new { id = productSupplier.ProductSupplierID }, productSupplier);
        }

        // DELETE: api/ProductSuppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSupplier(int id)
        {
            var productSupplier = await _context.ProductsSuppliers.FindAsync(id);
            if (productSupplier == null)
            {
                return NotFound();
            }

            _context.ProductsSuppliers.Remove(productSupplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductSupplierExists(int id)
        {
            return _context.ProductsSuppliers.Any(e => e.ProductSupplierID == id);
        }
    }
}
