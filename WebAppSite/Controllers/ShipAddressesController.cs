using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassLib.Data;
using ClassLib.Models;

namespace WebAppSite.Controllers
{
    public class ShipAddressesController : Controller
    {
        private readonly ClassLib.Data.AppContext _context;

        public ShipAddressesController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }

        // GET: ShipAddresses
        public async Task<IActionResult> Index()
        {
            var appContext = _context.ShipAddresses.Include(s => s.Customer);
            return View(await appContext.ToListAsync());
        }

        // GET: ShipAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipAddress = await _context.ShipAddresses
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.ShipAddressId == id);
            if (shipAddress == null)
            {
                return NotFound();
            }

            return View(shipAddress);
        }

        // GET: ShipAddresses/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return View();
        }

        // POST: ShipAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipAddressId,OrderId,Address,State,PostalCode,Country,Name,CustomerId")] ShipAddress shipAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", shipAddress.CustomerId);
            return View(shipAddress);
        }

        // GET: ShipAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipAddress = await _context.ShipAddresses.FindAsync(id);
            if (shipAddress == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", shipAddress.CustomerId);
            return View(shipAddress);
        }

        // POST: ShipAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipAddressId,OrderId,Address,State,PostalCode,Country,Name,CustomerId")] ShipAddress shipAddress)
        {
            if (id != shipAddress.ShipAddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipAddressExists(shipAddress.ShipAddressId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", shipAddress.CustomerId);
            return View(shipAddress);
        }

        // GET: ShipAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipAddress = await _context.ShipAddresses
                .Include(s => s.Customer)
                .FirstOrDefaultAsync(m => m.ShipAddressId == id);
            if (shipAddress == null)
            {
                return NotFound();
            }

            return View(shipAddress);
        }

        // POST: ShipAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipAddress = await _context.ShipAddresses.FindAsync(id);
            _context.ShipAddresses.Remove(shipAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipAddressExists(int id)
        {
            return _context.ShipAddresses.Any(e => e.ShipAddressId == id);
        }
    }
}
