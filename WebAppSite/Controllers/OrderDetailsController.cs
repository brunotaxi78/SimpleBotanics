using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassLib.Data;
using ClassLib.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using WebAppSite.Utils;
using System.Reflection;
using Newtonsoft.Json;
using WebAppSite.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;

namespace WebAppSite.Controllers
{

    public class OrderDetailsController : Controller
    {
        private readonly ClassLib.Data.AppContext _context;


        public OrderDetailsController(ClassLib.Data.AppContext context)
        {
            _context = context;
        }
        

        // GET: OrderDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderDetails.ToListAsync());
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        public IActionResult ShoppingCart()
        {
            string connection = "DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net";

            string table = "brunoShoppingCart";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);

            var cloudTable = storageAccount.CreateCloudTableClient().GetTableReference(table);

            string filtro = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, User.Identity.Name);

            TableQuery<AzureTablesManager> consulta = new TableQuery<AzureTablesManager>().Where(filtro);

            TableContinuationToken token = new TableContinuationToken();

            OrderDetail orderDetail = new OrderDetail();

            List<OrderDetail> orders = new List<OrderDetail>();

            foreach (AzureTablesManager te in cloudTable
                .ExecuteQuerySegmentedAsync(consulta, token).GetAwaiter().GetResult())
            {
                Type entityType = typeof(AzureTablesManager);
                PropertyInfo[] proplist = entityType.GetProperties();
                foreach (PropertyInfo pro in proplist)
                {
                    orderDetail = new OrderDetail();

                    orderDetail.ProductId = Convert.ToInt32(te.RowKey);

                    if (pro.Name == "properties")
                    {
                        IDictionary<string, EntityProperty> properties
                            = (IDictionary<string, EntityProperty>)pro.GetValue(te, null);
                        foreach (string key in properties.Keys)
                        {

                            if (key == "Name") orderDetail.Name = (string)properties[key].PropertyAsObject;
                            if (key == "Price") orderDetail.Price = Convert.ToDouble(properties[key].PropertyAsObject);
                            if (key == "Quantity") orderDetail.Quantity = Convert.ToInt32(properties[key].PropertyAsObject);
                            if (key == "ImageUrl") orderDetail.ImageUrl = (string)properties[key].PropertyAsObject;
                        }
                        
                    }
                    
                }
                orders.Add(orderDetail);
            }

            Customer customer = new Customer();

            List<Customer> customerList = new List<Customer>();

            customerList = _context.Customers.Where(c => c.Email == User.Identity.Name).ToList();

            ViewData["orderdetails"] = orders.ToList();

            ViewData["customerList"] = customerList.ToList();

            return View();
            }

            // GET: OrderDetails/Create
            public IActionResult Create()
            {
                return View();
            }

        public IActionResult AtivaBonus()
        {
            Customer customer = new Customer();

            List<Customer> customerList = new List<Customer>();

            customerList = _context.Customers.Where(c => c.Email == User.Identity.Name).ToList();

            customer.ValorBonus = 0;

            customerList.Add(customer);

            _context.Add(customer);

            _context.SaveChanges();

            return RedirectToAction("ShoppingCart", "OrderDetails");
        }

        public IActionResult CheckOut2()
        {
            string connection = "DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net";

            string table = "brunoShoppingCart";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);

            var cloudTable = storageAccount.CreateCloudTableClient().GetTableReference(table);

            string filtro = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, User.Identity.Name);

            TableQuery<AzureTablesManager> consulta = new TableQuery<AzureTablesManager>().Where(filtro);

            TableContinuationToken token = new TableContinuationToken();

            OrderDetail orderDetail = new OrderDetail();

            List<OrderDetail> orders = new List<OrderDetail>();

            Order order = new Order();

            List<Order> orderlist = new List<Order>();

            foreach (AzureTablesManager te in cloudTable
                .ExecuteQuerySegmentedAsync(consulta, token).GetAwaiter().GetResult())
            {
                Type entityType = typeof(AzureTablesManager);
                PropertyInfo[] proplist = entityType.GetProperties();
                foreach (PropertyInfo pro in proplist)
                {
                    orderDetail = new OrderDetail();

                    orderDetail.ProductId = Convert.ToInt32(te.RowKey);

                    orderDetail.User = User.Identity.Name;

                   


                    if (pro.Name == "properties")
                    {
                        IDictionary<string, EntityProperty> properties
                            = (IDictionary<string, EntityProperty>)pro.GetValue(te, null);
                        foreach (string key in properties.Keys)
                        {

                            if (key == "Name") orderDetail.Name = (string)properties[key].PropertyAsObject;
                            if (key == "Price") orderDetail.Price = Convert.ToDouble(properties[key].PropertyAsObject);
                            if (key == "Quantity") orderDetail.Quantity = Convert.ToInt32(properties[key].PropertyAsObject);
                            if (key == "ImageUrl") orderDetail.ImageUrl = (string)properties[key].PropertyAsObject;
                        }

                        orderDetail.Total = orderDetail.Price * orderDetail.Quantity;
                        order.Paid = order.Paid + orderDetail.Total;
                    }

                }
                
                order.Orderdate = DateTime.Now;
                order.PaymentDate = DateTime.Now;
                order.ShipDate = DateTime.Now;
                order.Freight = 5;
                order.SalesTax = 21;
                order.StatusId = 1;
                var email = User.Identity.Name.ToString();
                IEnumerable<Customer> teste = _context.Customers.ToList();
                foreach (var item in teste)
                {
                    if (item.Email == email) order.CustomerId = item.CustomerId;
                }


                orders.Add(orderDetail);
                _context.Add(orderDetail);
                orderlist.Add(order);
                _context.Add(order);
            }
            //_context.SaveChanges();
            Customer customer = new Customer();

            List<Customer> customerList = new List<Customer>();

            customerList = _context.Customers.Where(c => c.Email == User.Identity.Name).ToList();
            List<ShipAddress> shipAddressesList2 = new List<ShipAddress>();

            shipAddressesList2 = _context.ShipAddresses.Where(c => c.CustomerId == order.CustomerId).ToList();

            ViewData["orderdetails"] = orders.ToList();
            ViewData["orderlist"] = orderlist.ToList();
            ViewData["customerList"] = customerList.ToList();
            ViewData["shipAddressesList"] = shipAddressesList2.ToList();


            return View();
        }


        [HttpPost]
        public ActionResult FinalizarCompra(ShipAddress shipAdress)
        {

            string connection = "DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net";

            string table = "brunoShoppingCart";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);

            var cloudTable = storageAccount.CreateCloudTableClient().GetTableReference(table);

            string filtro = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, User.Identity.Name);

            TableQuery<AzureTablesManager> consulta = new TableQuery<AzureTablesManager>().Where(filtro);

            TableContinuationToken token = new TableContinuationToken();

            OrderDetail orderDetail = new OrderDetail();

            List<OrderDetail> orders = new List<OrderDetail>();

            Order order = new Order();

            List<Order> orderlist = new List<Order>();

            foreach (AzureTablesManager te in cloudTable
                .ExecuteQuerySegmentedAsync(consulta, token).GetAwaiter().GetResult())
            {
                Type entityType = typeof(AzureTablesManager);
                PropertyInfo[] proplist = entityType.GetProperties();
                foreach (PropertyInfo pro in proplist)
                {
                    orderDetail = new OrderDetail();

                    orderDetail.ProductId = Convert.ToInt32(te.RowKey);

                    orderDetail.User = User.Identity.Name;




                    if (pro.Name == "properties")
                    {
                        IDictionary<string, EntityProperty> properties
                            = (IDictionary<string, EntityProperty>)pro.GetValue(te, null);
                        foreach (string key in properties.Keys)
                        {

                            if (key == "Name") orderDetail.Name = (string)properties[key].PropertyAsObject;
                            if (key == "Price") orderDetail.Price = Convert.ToDouble(properties[key].PropertyAsObject);
                            if (key == "Quantity") orderDetail.Quantity = Convert.ToInt32(properties[key].PropertyAsObject);
                            if (key == "ImageUrl") orderDetail.ImageUrl = (string)properties[key].PropertyAsObject;
                        }

                        orderDetail.Total = orderDetail.Price * orderDetail.Quantity;
                        order.Paid = order.Paid + orderDetail.Total;
                    }

                }

                order.Orderdate = DateTime.Now;
                order.PaymentDate = DateTime.Now;
                order.ShipDate = DateTime.Now;
                order.Freight = 5;
                order.SalesTax = 21;
                order.StatusId = 1;
                var email = User.Identity.Name.ToString();

                IEnumerable<Customer> teste = _context.Customers.ToList();
                foreach (var item in teste)
                {
                    if (item.Email == email)
                    {
                        order.CustomerId = item.CustomerId;
                        shipAdress.CustomerId = item.CustomerId;
                        shipAdress.Name = item.CustomerName;
                    }
                }

                _context.Add(shipAdress);

                IEnumerable<ShipAddress> shipAddressesList = _context.ShipAddresses.ToList();

                var exits = shipAddressesList.Where(sa => sa.CustomerId == order.CustomerId).Count();

                if(exits==0) _context.SaveChanges();

                orders.Add(orderDetail);
                orderlist.Add(order);
            }
            //_context.SaveChanges();
            Customer customer = new Customer();
            List<Customer> customerList = new List<Customer>();

            customerList = _context.Customers.Where(c => c.Email == User.Identity.Name).ToList();

            //List<ShipAddress> shipAddressesList2 = new List<ShipAddress>();

            //shipAddressesList2 = _context.ShipAddresses.Where(c => c.CustomerId == order.CustomerId).ToList();
            List<CreditCard> creditCardList = new List<CreditCard>();

            creditCardList = _context.CreditCards.Where(c => c.CustomerId == order.CustomerId).ToList();

            ViewData["orderdetails"] = orders.ToList();
            ViewData["orderlist"] = orderlist.ToList();
            ViewData["customerList"] = customerList.ToList();
            ViewData["creditCardList"] = creditCardList.ToList();


            return View();
        }

        [HttpPost]
        public IActionResult AtualizaValores([FromBody] string dados)
        {
            string[] info = dados.Split(":");

            var valorFinal = info[0];
            var desconto = info[1];


            //var val = (float)_customer.ValorBonus;


            //var customer = _context.Customers.First(c => c.CustomerId == _customer.CustomerId);
            //customer.ValorBonus = val;
            //customer.NumBonus = customer.NumBonus - 1;
            //_context.Update(customer);

            //_context.SaveChanges();

            //return Json(data: new { success = true });

            return Json(data: new { success = true });

        }

        [HttpPost]
        public ActionResult TerminaCompra([FromBody] string dados)
        {

            string[] info = dados.Split(":");



            string connection = "DefaultEndpointsProtocol=https;AccountName=asprojeto;AccountKey=a0B+PPewtIG4+ngBo/4uXdEnNq/RGCvVESJat3kcNOdmYTydATc8ik9Y7oumfAJOEJXvfyF5lP3zjOGROOPguA==;EndpointSuffix=core.windows.net";

            string table = "brunoShoppingCart";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);

            var cloudTable = storageAccount.CreateCloudTableClient().GetTableReference(table);

            string filtro = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, User.Identity.Name);

            TableQuery<AzureTablesManager> consulta = new TableQuery<AzureTablesManager>().Where(filtro);

            TableContinuationToken token = new TableContinuationToken();

            OrderDetail orderDetail = new OrderDetail();

            List<OrderDetail> orders = new List<OrderDetail>();

            Order order = new Order();

            List<Order> orderlist = new List<Order>();

            CreditCard creditCard = new CreditCard();

            foreach (AzureTablesManager te in cloudTable
                .ExecuteQuerySegmentedAsync(consulta, token).GetAwaiter().GetResult())
            {
                Type entityType = typeof(AzureTablesManager);
                PropertyInfo[] proplist = entityType.GetProperties();
                foreach (PropertyInfo pro in proplist)
                {
                    orderDetail = new OrderDetail();

                    orderDetail.ProductId = Convert.ToInt32(te.RowKey);

                    orderDetail.User = User.Identity.Name;




                    if (pro.Name == "properties")
                    {
                        IDictionary<string, EntityProperty> properties
                            = (IDictionary<string, EntityProperty>)pro.GetValue(te, null);
                        foreach (string key in properties.Keys)
                        {

                            if (key == "Name") orderDetail.Name = (string)properties[key].PropertyAsObject;
                            if (key == "Price") orderDetail.Price = Convert.ToDouble(properties[key].PropertyAsObject);
                            if (key == "Quantity") orderDetail.Quantity = Convert.ToInt32(properties[key].PropertyAsObject);
                            if (key == "ImageUrl") orderDetail.ImageUrl = (string)properties[key].PropertyAsObject;
                        }

                        orderDetail.Total = orderDetail.Price * orderDetail.Quantity;
                        order.Paid = int.Parse(info[0]) - int.Parse(info[1]);
                    }

                }

                order.Orderdate = DateTime.Now;
                order.PaymentDate = DateTime.Now;
                order.ShipDate = DateTime.Now;
                order.Freight = 5;
                order.Discount = int.Parse(info[1]);
                order.SalesTax = 21;
                order.StatusId = 3;
                
                var email = User.Identity.Name.ToString();
                
                IEnumerable<Customer> teste = _context.Customers.ToList();
                foreach (var item in teste)
                {
                    if (item.Email == email)
                    {
                        order.CustomerId = item.CustomerId;
                        creditCard.CustomerId = item.CustomerId;
                        item.NumBonus += 1;
                        if(item.ValorBonus>0) item.ValorBonus -= int.Parse(info[1]);
                        _context.Customers.Update(item);
                        //_context.SaveChanges();
                    }
                }

                creditCard.CardName = info[2];
                creditCard.CardNumber = int.Parse(info[3]);
                creditCard.ExpDate = info[4];
                creditCard.SecurityNumber = info[5];
                creditCard.CardType = info[6];


                _context.Add(creditCard);
                orders.Add(orderDetail);
                _context.Add(orderDetail);
                orderlist.Add(order);
                _context.Add(order);
            }
            _context.SaveChanges();
            

            foreach (AzureTablesManager te in cloudTable
                .ExecuteQuerySegmentedAsync(consulta, token).GetAwaiter().GetResult())
            {
                te.TableCartDelete(Convert.ToInt32(te.RowKey), User.Identity.Name);
            }


            return Json(data: new { success = true });
            //return RedirectToAction("Index2","Products");
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,Price,Quantity,Tax,Discount,Total")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        [HttpPost]
        public IActionResult Create2([FromBody] OrderDetail orderDetail)
        {
        if (User.Identity.IsAuthenticated)
        {

            AzureTablesManager add = new AzureTablesManager();
            add.IfExists(orderDetail);

            //Criar Registo
            //AzureTablesManager newte = new AzureTablesManager(User.Identity.Name.ToString(), orderDetail.ProductId.ToString());

            //newte.properties.Add("Name", newte.ConvertToEntityProperty("Name", orderDetail.Name.ToString()));
            //newte.properties.Add("Price", newte.ConvertToEntityProperty("Price", orderDetail.Price.ToString()));
            //newte.properties.Add("Quantity", newte.ConvertToEntityProperty("Quantity", orderDetail.Quantity.ToString()));
            //newte.properties.Add("ImageUrl", newte.ConvertToEntityProperty("ImageUrl", orderDetail.ImageUrl.ToString()));
            //newte.properties.Add("Total", newte.ConvertToEntityProperty("Total", orderDetail.Total.ToString()));

            //newte.ConnectAzure().ExecuteAsync(TableOperation.InsertOrMerge(newte));

            //OrderDetail order = new OrderDetail
            //{
            //    Name = orderDetail.Name,
            //    Quantity = orderDetail.Quantity,
            //    ImageUrl = orderDetail.ImageUrl,
            //    Price = orderDetail.Price,
            //    Total = orderDetail.Total
            //};

            //_context.Add(order);
            //_context.SaveChanges();

            return Json(data: new { success = true, message = "Produto Adicionado" });
        }
        else
        {
            return View("Login","Account");
        }
        }

        [HttpPost]
        public IActionResult Delete2([FromBody]Teste obj)
        {
            var id = obj.id;
            var user = obj.user;
            AzureTablesManager add = new AzureTablesManager();
            add.TableItemDelete(Convert.ToInt32(id), user);

           return Json(data: new { success = true, message = "Produto Removido" });
        }

        public class Teste
        {
            public string id { get; set; }

            public string user { get; set; }
        }

        [HttpPost]
        public IActionResult CountShoppingCart([FromBody]Teste obj)
        {
            var id = obj.id;
            var user = obj.user;

            AzureTablesManager count = new AzureTablesManager();
            int aaa = count.CountShoppingCart(user);

            return Json(data: new { success = true, message = aaa });
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var orderDetail = await _context.OrderDetails.FindAsync(id);
                if (orderDetail == null)
                {
                    return NotFound();
                }
                return View(orderDetail);
            }

            // POST: OrderDetails/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,Price,Quantity,Tax,Discount,Total")] OrderDetail orderDetail)
            {
                if (id != orderDetail.OrderDetailId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(orderDetail);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OrderDetailExists(orderDetail.OrderDetailId))
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
                return View(orderDetail);
            }

            // GET: OrderDetails/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var orderDetail = await _context.OrderDetails
                    .FirstOrDefaultAsync(m => m.OrderDetailId == id);
                if (orderDetail == null)
                {
                    return NotFound();
                }

                return View(orderDetail);
            }

            // POST: OrderDetails/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var orderDetail = await _context.OrderDetails.FindAsync(id);
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool OrderDetailExists(int id)
            {
                return _context.OrderDetails.Any(e => e.OrderDetailId == id);
            }
        }
    }
