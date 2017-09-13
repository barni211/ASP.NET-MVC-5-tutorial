using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;
using System.Linq;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products =
         {
                new Product {Name = "Kajak", Price = 275M },
                new Product {Name = "Kamizelka ratunkowa", Price = 48.95M },
                new Product {Name = "Piłka nozna", Price = 19.50M },
                new Product {Name = "Flaga narożna", Price = 34.95M }
            };
        [TestMethod]
        public void SumProductsCorrectly()
        {
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            var target = new LinqValueCalculator(mock.Object);

            var result = target.ValueProducts(products);

            Assert.AreEqual(products.Sum(e=>e.Price), result);
        }

        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void PassThroughVariableDiscounts()
        {
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<System.ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9M));
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v <= 100 && v >= 10))).Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculator(mock.Object);

            //work
            decimal FiveDollarDiscount = target.ValueProducts(createProduct(5));
            decimal TenDollarDiscount = target.ValueProducts(createProduct(10));
            decimal FiftyDollarDiscount = target.ValueProducts(createProduct(50));
            decimal HundredDollarDiscount = target.ValueProducts(createProduct(100));
            decimal FiveHundredDollarDiscount = target.ValueProducts(createProduct(500));

            Assert.AreEqual(5, FiveDollarDiscount, "Niepowodzenie 5 zł");
            Assert.AreEqual(5, TenDollarDiscount, "Niepowodzenie 10 zł");
            Assert.AreEqual(45, FiftyDollarDiscount, "Niepowodzenie 50 zł");
            Assert.AreEqual(95, HundredDollarDiscount, "Niepowodzenie 100 zł");
            Assert.AreEqual(450, FiveHundredDollarDiscount, "Niepowodzenie 500 zł");
            target.ValueProducts(createProduct(0));


        }
    }
}
