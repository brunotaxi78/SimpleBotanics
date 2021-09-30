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
    public class BillingAddressesController : ControllerBase
    {
        private readonly ClassLib.Data.AppContext _context;

        public BillingAddressesController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }

        // GET: api/BillingAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillingAddress>>> GetBillingAddresses()
        {
            return await _context.BillingAddresses.ToListAsync();
        }

        // GET: api/BillingAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillingAddress>> GetBillingAddress(int id)
        {
            var billingAddress = await _context.BillingAddresses.FindAsync(id);

            if (billingAddress == null)
            {
                return NotFound();
            }

            return billingAddress;
        }

        // PUT: api/BillingAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillingAddress(int id, BillingAddress billingAddress)
        {
            if (id != billingAddress.BillingAddressId)
            {
                return BadRequest();
            }

            _context.Entry(billingAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingAddressExists(id))
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

        // POST: api/BillingAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillingAddress>> PostBillingAddress(BillingAddress billingAddress)
        {
            _context.BillingAddresses.Add(billingAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillingAddress", new { id = billingAddress.BillingAddressId }, billingAddress);
        }

        // DELETE: api/BillingAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillingAddress(int id)
        {
            var billingAddress = await _context.BillingAddresses.FindAsync(id);
            if (billingAddress == null)
            {
                return NotFound();
            }

            _context.BillingAddresses.Remove(billingAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillingAddressExists(int id)
        {
            return _context.BillingAddresses.Any(e => e.BillingAddressId == id);
        }
    }
}
