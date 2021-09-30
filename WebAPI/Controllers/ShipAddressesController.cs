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
    public class ShipAddressesController : ControllerBase
    {
        private readonly ClassLib.Data.AppContext _context;

        public ShipAddressesController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }

        // GET: api/ShipAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipAddress>>> GetShipAddresses()
        {
            return await _context.ShipAddresses.ToListAsync();
        }

        // GET: api/ShipAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipAddress>> GetShipAddress(int id)
        {
            var shipAddress = await _context.ShipAddresses.FindAsync(id);

            if (shipAddress == null)
            {
                return NotFound();
            }

            return shipAddress;
        }

        // PUT: api/ShipAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipAddress(int id, ShipAddress shipAddress)
        {
            if (id != shipAddress.ShipAddressId)
            {
                return BadRequest();
            }

            _context.Entry(shipAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipAddressExists(id))
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

        // POST: api/ShipAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShipAddress>> PostShipAddress(ShipAddress shipAddress)
        {
            _context.ShipAddresses.Add(shipAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShipAddress", new { id = shipAddress.ShipAddressId }, shipAddress);
        }

        // DELETE: api/ShipAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipAddress(int id)
        {
            var shipAddress = await _context.ShipAddresses.FindAsync(id);
            if (shipAddress == null)
            {
                return NotFound();
            }

            _context.ShipAddresses.Remove(shipAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShipAddressExists(int id)
        {
            return _context.ShipAddresses.Any(e => e.ShipAddressId == id);
        }
    }
}
