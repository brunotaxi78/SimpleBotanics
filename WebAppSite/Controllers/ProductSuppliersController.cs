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
    public class ProductSuppliersController : Controller
    {
        private readonly ClassLib.Data.AppContext _context;

        public ProductSuppliersController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }

        // GET: ProductSuppliers
        public async Task<IActionResult> Index()
        {
            var appContext = _context.ProductsSuppliers.Include(p => p.Product).Include(p => p.Supplier);
            return View(await appContext.ToListAsync());
        }

        // GET: ProductSuppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSupplier = await _context.ProductsSuppliers
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductSupplierID == id);
            if (productSupplier == null)
            {
                return NotFound();
            }

            return View(productSupplier);
        }

        // GET: ProductSuppliers/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductId");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId");
            return View();
        }

        // POST: ProductSuppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductSupplierID,ProductID,SupplierID")] ProductSupplier productSupplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productSupplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductId", productSupplier.ProductID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", productSupplier.SupplierID);
            return View(productSupplier);
        }

        // GET: ProductSuppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSupplier = await _context.ProductsSuppliers.FindAsync(id);
            if (productSupplier == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductId", productSupplier.ProductID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", productSupplier.SupplierID);
            return View(productSupplier);
        }

        // POST: ProductSuppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductSupplierID,ProductID,SupplierID")] ProductSupplier productSupplier)
        {
            if (id != productSupplier.ProductSupplierID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSupplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSupplierExists(productSupplier.ProductSupplierID))
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
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductId", productSupplier.ProductID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId", productSupplier.SupplierID);
            return View(productSupplier);
        }

        // GET: ProductSuppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSupplier = await _context.ProductsSuppliers
                .Include(p => p.Product)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductSupplierID == id);
            if (productSupplier == null)
            {
                return NotFound();
            }

            return View(productSupplier);
        }

        // POST: ProductSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSupplier = await _context.ProductsSuppliers.FindAsync(id);
            _context.ProductsSuppliers.Remove(productSupplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSupplierExists(int id)
        {
            return _context.ProductsSuppliers.Any(e => e.ProductSupplierID == id);
        }
    }
}
