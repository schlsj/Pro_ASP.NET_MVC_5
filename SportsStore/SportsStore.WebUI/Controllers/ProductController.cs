﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public int PageSize = 4;

        public ProductController(IProductsRepository productRepository)
        {
            repository = productRepository;
        }

        public ViewResult List(string category,int page=1)
        {
            //return View(repository.Products.OrderBy(a => a.ProductID).Skip((page - 1) * PageSize).Take(PageSize));
            ProductsListViewModel model = new ProductsListViewModel()
            {
                Products = repository.Products.Where(a => category == null || a.Category == category).OrderBy(a => a.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(a => a.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}