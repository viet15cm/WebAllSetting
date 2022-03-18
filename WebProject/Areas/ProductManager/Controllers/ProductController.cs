using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models;
using WebProject.Services;

namespace WebProject.Areas.ProductManager.Controllers
{
    [Area("ProductManager")]
   
    public class ProductController : Controller
    {
        private static string contenRootPath { get; set; }
        private readonly ProductServices _products;
        public ProductController(ProductServices productServices, IWebHostEnvironment webHostEnvironment)
        {
            _products = productServices;
            contenRootPath = webHostEnvironment.ContentRootPath;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _products.GetAll());
        }

        //    Kiểu trả về                 | Phương thức
        //------------------------------------------------
        //ContentResult               | Content()
        //EmptyResult                 | new EmptyResult()
        //FileResult                  | File()
        //ForbidResult                | Forbid()
        //JsonResult                  | Json()
        //LocalRedirectResult         | LocalRedirect()
        //RedirectResult              | Redirect()
        //RedirectToActionResult      | RedirectToAction()
        //RedirectToPageResult        | RedirectToRoute()
        //RedirectToRouteResult       | RedirectToPage()
        //PartialViewResult           | PartialView()
        //ViewComponentResult         | ViewComponent()
        //StatusCodeResult            | StatusCode()
        //ViewResult                  | View()

        public IActionResult ShowImg()
        {
            string filePath = Path.Combine(contenRootPath, "FileImg", "hinh.jpg");
            var bytes = System.IO.File.ReadAllBytes(filePath);


            return File(bytes, "image/jpg");
        }

        public IActionResult NokiaPrice()
        {
            return Json(new
            {
                Name = "Nokia",
                price = 124.4
            });
        }

        public IActionResult Navigate()
        {
            var url = Url.Action("index", "product");

            return LocalRedirect(url);
        }

        public IActionResult Google()
        {
            var url = "https://www.google.com/";

            return Redirect(url);
        }

        public IActionResult FindProduct(Guid? id)
        {
            var product = (List<Product>)_products.GetAll().Result;

            var findProduct = product.Where(x => x.Id == id).FirstOrDefault();

            return (findProduct == null) ? Content($"Khong co san pham ") : Content($"San pham {findProduct.Name}  co id = {findProduct.Id}");
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult ViewsProduct(Guid? id)
        {
            var product = (List<Product>)_products.GetAll().Result;

            var findProduct = product.Where(x => x.Id == id).FirstOrDefault();
            if (findProduct != null)
            {
                StatusMessage = findProduct.Name;

                return View(findProduct);
            }

            return NotFound();
        }
    }
}
