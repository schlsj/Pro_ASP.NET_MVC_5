using System;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private Product[] products = {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275m},
            new Product {Name = "Lifejacket", Category = "watersports", Price = 48.95m},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50m},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95m}
        };

        [TestMethod]
        public void Sum_Products_Correctly()
        {
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(a => a.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(a => a);
            var target = new LinqValueCalculator(mock.Object);
            //var discounter=new MinimumDiscountHelper();
            //var target = new LinqValueCalculator(discounter);
            //var goalTotal = products.Sum(a => a.Price);

            var result = target.ValueProducts(products);

            //Assert.AreEqual(goalTotal, result);
            Assert.AreEqual(products.Sum(a => a.Price), result);
        }

        Product[] CreateProduct(decimal value)
        {
            return new[] {new Product {Price = value}};
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<ArgumentOutOfRangeException>();
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9m));
            mock.Setup(m => m.ApplyDiscount(It.IsInRange(10m, 100m, Range.Inclusive)))
                .Returns<decimal>(total => total - 5);
            var target = new LinqValueCalculator(mock.Object);

            decimal fiveDiscount = target.ValueProducts(CreateProduct(5));
            decimal tenDiscount = target.ValueProducts(CreateProduct(10));
            decimal fiftyDiscount = target.ValueProducts(CreateProduct(50));
            decimal hundredDiscount = target.ValueProducts(CreateProduct(100));
            decimal fivehundredDiscount = target.ValueProducts(CreateProduct(500));

            Assert.AreEqual(5, fiveDiscount, "$5 Fail");
            Assert.AreEqual(5, tenDiscount, "$10 Fail");
            Assert.AreEqual(45, fiftyDiscount, "$50 Fail");
            Assert.AreEqual(95, hundredDiscount, "$100 Fail");
            Assert.AreEqual(450, fivehundredDiscount, "$500 Fail");
            target.ValueProducts(CreateProduct(0));
        }

    }
}
