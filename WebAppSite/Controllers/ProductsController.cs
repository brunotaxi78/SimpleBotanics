using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassLib.Data;
using ClassLib.Models;
using System.Dynamic;
using WebAppSite.Utils;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Data.SqlClient.Server;
using System.Web;

namespace WebAppSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ClassLib.Data.AppContext _context;

        public ProductsController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }


        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {


            AzureStorageManager File2 = new AzureStorageManager();

            string url = File2.Upload(file);

            ViewBag.Url = url;

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "OrderDetailId");

            return View("Create");

        }

        [HttpPost]
        public IActionResult UploadEdit(IFormFile file)
        {

            AzureStorageManager File2 = new AzureStorageManager();

            string url = File2.Upload(file);

            ViewBag.Url = url;

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "OrderDetailId");

            return View("Create");
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Products.Include(p => p.Category);
            return View(await appContext.ToListAsync());
        }

        public ActionResult Index2()
        {

            ViewData["Products"] =  _context.Products.ToList();
            ViewData["Categories"] = _context.Categories.ToList();


            return View();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductsDetails
        public async Task<IActionResult> ProductDetails(int? id)
        {
            AzureQueueManager queue = new AzureQueueManager();

           

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            queue.QueueSend(product);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult GetCatProducts(int? catId)
        {

            if (catId == null)
            {
                return NotFound();
            }

            var product = _context.Products.Where(prod => prod.CategoryId == catId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["OrderDetailId"] = new SelectList(_context.OrderDetails, "OrderDetailId", "OrderDetailId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,SKU,Description,Price,Size,Stock,Color,Picture,Ranking,CategoryId")] Product product, IFormFile file)
        {
         

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,SKU,Description,Price,Size,Stock,Color,Picture,Ranking,CategoryId")] Product product)
        {

            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
