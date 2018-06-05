using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            Product prod=new Product()
            {
                ProductID = 2,
                Name = "Test",
                ImageData = new byte[] {},
                ImageMimeType = "image/png"
            };
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(a => a.Products).Returns(new Product[]
            {
                new Product() {ProductID = 1, Name = "P1"},
                prod,
                new Product() {ProductID = 3, Name = "P3"},
            }.AsQueryable());
            ProductController target = new ProductController(mock.Object);

            ActionResult result = target.GetImage(2);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prod.ImageMimeType, ((FileResult) result).ContentType);
        }
    }
}
