using Microsoft.AspNetCore.Mvc;
using PRODUCTsession.Models;

namespace PRODUCTsession.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var productModel = new ProductModel();
            ViewBag.products = productModel.findAll();
            return View();
        }
    }
}
