using Microsoft.AspNetCore.Mvc;
using PRODUCTsession.Helper;
using PRODUCTsession.Models;

namespace PRODUCTsession.Controllers
{
    public class CartController : Controller
    {
        private List<Item>? sales;
        public List<Item> save()
        {
            //      sales.Clear();
            try
            {
              
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                
                if (cart != null)
                {
                    sales = cart.ToList();
                    HttpContext.Session.Remove("cart");
                }
               
                return sales;
            }
            catch
            {
                throw;
            }

      
        }
       
         //   [Route("index")]
            public IActionResult Index()
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                var cartList = cart.ToList();
                ViewBag.cart = cartList;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
                return View();
            }

            return View();
           // return RedirectToAction("/Product/Index");

        }

     //   [Route("Invoice")]
        public IActionResult Invoice()
        { 
            
            try
            {
               ViewBag.ListCart = null;
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");


                if (cart == null)
                {
                    ViewBag.ListCart = null;


                }
                else
                {
                    var ListCartSave = save();
                    ViewBag.ListCart = ListCartSave;
                }

                return View(ViewBag.ListCart);
            }
            catch
            {
                throw;
            }
            
        }

    //    [Route("buy/{id}")]
            public IActionResult Buy(int id)
            {
                ProductModel productModel = new ProductModel();
                if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
                {
                    List<Item> cart = new List<Item>();
                    cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                else
                {
                    List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                    int index = isExist(id);
                    if (index != -1)
                    {
                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new Item { Product = productModel.find(id), Quantity = 1 });
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                return RedirectToAction("Index");
            }

        //    [Route("remove/{id}")]
            public IActionResult Remove(int id)
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                return RedirectToAction("Index");
            }

            private int isExist(int id)
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Product.Id.Equals(id))
                    {
                        return i;
                    }
                }
                return -1;
            }

        }
    }
