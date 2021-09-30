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
    public class OrdersController : Controller
    {
        private readonly ClassLib.Data.AppContext _context;

        public OrdersController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Orders.Include(o => o.BillingAddress).Include(o => o.Customer).Include(o => o.ShipAddress).Include(o => o.Status);
            return View(await appContext.ToListAsync());
        }

        public async Task<IActionResult> ClientOrders()
        {
            var appContext = _context.Orders.Include(o => o.BillingAddress).Include(o => o.Customer).Include(o => o.ShipAddress).Include(o => o.Status);

            return View(await appContext.ToListAsync());

         
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.BillingAddress)
                .Include(o => o.Customer)
                .Include(o => o.ShipAddress)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult CreateFromProduct()
        {
            var newOrder = new Order();
            newOrder.Orderdate = DateTime.Now;
            newOrder.Status.StatusId = 1;
            _context.Add(newOrder);
            _context.SaveChangesAsync();

            return RedirectToAction("Index2","Products");
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["BillingAddressId"] = new SelectList(_context.BillingAddresses, "BillingAddressId", "BillingAddressId");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "ShipAddressId", "ShipAddressId");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,PaymentId,Orderdate,ShipDate,Freight,SalesTax,Paid,PaymentDate,CustomerId,BillingAddressId,ShipAddressId,StatusId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillingAddressId"] = new SelectList(_context.BillingAddresses, "BillingAddressId", "BillingAddressId", order.BillingAddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "ShipAddressId", "ShipAddressId", order.ShipAddressId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", order.StatusId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["BillingAddressId"] = new SelectList(_context.BillingAddresses, "BillingAddressId", "BillingAddressId", order.BillingAddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "ShipAddressId", "ShipAddressId", order.ShipAddressId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", order.StatusId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,PaymentId,Orderdate,ShipDate,Freight,SalesTax,Paid,PaymentDate,CustomerId,BillingAddressId,ShipAddressId,StatusId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["BillingAddressId"] = new SelectList(_context.BillingAddresses, "BillingAddressId", "BillingAddressId", order.BillingAddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["ShipAddressId"] = new SelectList(_context.ShipAddresses, "ShipAddressId", "ShipAddressId", order.ShipAddressId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", order.StatusId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.BillingAddress)
                .Include(o => o.Customer)
                .Include(o => o.ShipAddress)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
