using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product { productId = "100", Name="Kajak", description = "Łódka jednoosobowa", price = 275M, category = "Sporty wodne" };

            ShoppingCart cart = new ShoppingCart { products = new List<Product>() };
            cart.products.Add(myProduct);
            cart.products.Add(new Product { price = 300M });
            myProduct.Name = "Kajak";

            string productName = myProduct.Name;

            decimal cartTotal = MyExtensionMetods.TotalPrices(cart);

            return View("Result", (object)String.Format("Totalna wartosc: {0}", cartTotal));
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                products = new List<Product>
                {
                    new Product { Name = "Kajak", category = "Sporty wodne", price= 270M },
                    new Product { Name = "Kamizelka ratunkowa", category = "Sporty wodne", price= 125.50M },
                    new Product { Name = "Piłka nożna", category = "Piłka nożna", price= 270M },
                    new Product { Name = "Flaga narodowa", category = "Piłka nożna", price= 350M }
                }

            };

            decimal total = 0;
            foreach ( Product prod in products.FilterByCategory("Piłka nożna"))
            {
                total += prod.price;
            }

            return View("Result", (object)String.Format("Razem: {0}", total));
        }

        public ViewResult UseExtensionFilter()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                products = new List<Product>
                {
                    new Product { Name = "Kajak", category = "Sporty wodne", price= 270M },
                    new Product { Name = "Kamizelka ratunkowa", category = "Sporty wodne", price= 125.50M },
                    new Product { Name = "Piłka nożna", category = "Piłka nożna", price= 27M },
                    new Product { Name = "Flaga narodowa", category = "Piłka nożna", price= 35.50M }
                }

            };


            //Func<Product, bool> categoryFilter = prod => prod.category == "Piłka nożna";
           

            decimal total = 0;
            foreach (Product prod in products.Filter(prod => prod.category == "Piłka nożna"))
            {
                total += prod.price;
            }

            return View("Result", (object)String.Format("Razem: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {

            var myAnonItem = new { Name = "MVC", Category = "Wzorzec" };
            var oddsAndEnds = new[]
            {
                new { Name = "MVC", Category = "Wzorzec"},
                new { Name = "Kapelusz", Category = "Odziez"},
                new { Name = "Jablko", Category = "Owoc"}
            };

            StringBuilder result = new StringBuilder();
            foreach(var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" \n");
            }

            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProducts()
        {
            Product[] products =
                {
                    new Product { Name = "Kajak", category = "Sporty wodne", price= 270M },
                    new Product { Name = "Kamizelka ratunkowa", category = "Sporty wodne", price= 125.50M },
                    new Product { Name = "Piłka nożna", category = "Piłka nożna", price= 27M },
                    new Product { Name = "Flaga narodowa", category = "Piłka nożna", price= 35.50M }
                };

            //var foundProducts = from match in products
            //                    orderby match.price descending
            //                    select new { match.Name, match.price };

            var foundProducts = products.OrderByDescending(e => e.price).
                                Take(3).
                                Select(e => new { e.Name, e.price });

            products[2] = new Product { Name = "Stadion", price = 79500M };

            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Cena: {0} ", p.price);
                if (++count == 3)
                {
                    break;
                }
            }

            return View("Result", (object)result.ToString());
        }

        public ViewResult getAsyncTask()
        {
            
            return View("Result", (object)MyAsyncMethods.GetPageLength().ToString());
        }
    }
}