using ClassLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebAppSite.Models;

namespace WebAppSite.Controllers
{
    public class HomeController : Controller
    {

        private readonly ClassLib.Data.AppContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ClassLib.Data.AppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Jogo()
        {


            var customerList = _context.Customers.Where(c => c.Email == User.Identity.Name).ToList();

            ViewData["customerList"] = customerList.ToList();

            return View();
        }


        [HttpPost]
        public IActionResult Jogo([FromBody] Customer _customer)
        {

            
            var val = (float)_customer.ValorBonus;


            var customer = _context.Customers.First(c => c.CustomerId == _customer.CustomerId);
            customer.ValorBonus = val;
            customer.NumBonus = customer.NumBonus - 1;
            _context.Update(customer);
            
            _context.SaveChanges();

            return Json(data: new { success = true });
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(SendEmailDto sendEmailDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new();
                    mail.From = new MailAddress("grupo3.rumos@gmail.com");
                    mail.To.Add("grupo3.rumos@gmail.com");
                    mail.Subject = sendEmailDto.Subject;
                    string content = sendEmailDto.Message + "<br /><br /> Sender Email : " + sendEmailDto.Email;
                    mail.Body = content;
                    mail.IsBodyHtml = true;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential("grupo3.rumos@gmail.com", "Rumos123++"); // Enter seders User name and password  
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    
                    ModelState.Clear();

                    TempData["Message"] = "Email sent";

                }
                catch (Exception ex)
                {

                    ViewBag.Message = ex.Message.ToString();
                }
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
