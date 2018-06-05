﻿using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product {Name = "Footbal", Price = 25},
            //    new Product {Name = "Surf board", Price = 179},
            //    new Product {Name = "Running shoes", Price = 95}
            //});
            //kernel.Bind<IProductsRepository>().ToConstant(mock.Object);

            kernel.Bind<IProductsRepository>().To<EFProductRepository>();
            EmailSettings emailSettings=new EmailSettings()
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"]??"false")
            };
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}