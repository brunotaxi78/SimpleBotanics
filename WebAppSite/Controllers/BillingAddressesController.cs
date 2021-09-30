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
    public class BillingAddressesController : Controller
    {
        private readonly ClassLib.Data.AppContext _context;

        public BillingAddressesController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }

        // GET: BillingAddresses
        public async Task<IActionResult> Index()
        {
            var appContext = _context.BillingAddresses.Include(b => b.Customer);
            return View(await appContext.ToListAsync());
        }

        // GET: BillingAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAddress = await _context.BillingAddresses
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BillingAddressId == id);
            if (billingAddress == null)
            {
                return NotFound();
            }

            return View(billingAddress);
        }

        // GET: BillingAddresses/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return View();
        }

        // POST: BillingAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillingAddressId,Address,State,PostalCode,Country,Name,CustomerId")] BillingAddress billingAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billingAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", billingAddress.CustomerId);
            return View(billingAddress);
        }

        // GET: BillingAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAddress = await _context.BillingAddresses.FindAsync(id);
            if (billingAddress == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", billingAddress.CustomerId);
            return View(billingAddress);
        }

        // POST: BillingAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillingAddressId,Address,State,PostalCode,Country,Name,CustomerId")] BillingAddress billingAddress)
        {
            if (id != billingAddress.BillingAddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billingAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingAddressExists(billingAddress.BillingAddressId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", billingAddress.CustomerId);
            return View(billingAddress);
        }

        // GET: BillingAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAddress = await _context.BillingAddresses
                .Include(b => b.Customer)
                .FirstOrDefaultAsync(m => m.BillingAddressId == id);
            if (billingAddress == null)
            {
                return NotFound();
            }

            return View(billingAddress);
        }

        // POST: BillingAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billingAddress = await _context.BillingAddresses.FindAsync(id);
            _context.BillingAddresses.Remove(billingAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillingAddressExists(int id)
        {
            return _context.BillingAddresses.Any(e => e.BillingAddressId == id);
        }
    }
}
