using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EssentialTools.Models;
using Ninject;

namespace EssentialTools.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private IValueCalculator calc;
        private Product[] array =
            {
                new Product {Name = "Kajak", Price = 275M },
                new Product {Name = "Kamizelka ratunkowa", Price = 48.95M },
                new Product {Name = "Piłka nozna", Price = 19.50M },
                new Product {Name = "Flaga narożna", Price = 34.95M }
            };

        public HomeController(IValueCalculator calcParam, IValueCalculator calcParam2)
        {
            calc = calcParam;
        }
        public ActionResult Index()
        {
            ShoppingCart cart = new ShoppingCart(calc) { Products = array };

            decimal totalValue = cart.CalculateProductTotal();

            return View(totalValue);
        }
    }
}