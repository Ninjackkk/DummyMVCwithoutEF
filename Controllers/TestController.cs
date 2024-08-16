using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DummyMVCwithoutEF.Models;

namespace DummyMVCwithoutEF.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        ProductDal pd=new ProductDal();
        public ActionResult Index()
        {
            var data=pd.GetAllProducts();
            return View(data);
        }


        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
           pd.AddProduct(p);
           TempData["success"] = "Product Added Successfully";
           return RedirectToAction("AddProduct");
        }

        public ActionResult DeleteProd(int id)
        {
            pd.DeleteProd(id);
            TempData["Deleted"] = "Product Deleted Successfully";
            return RedirectToAction("Index");
        }


      

        public ActionResult EditProd(int id, string name, string cat, double price)
        {
            pd.EditProd(id,name,cat,price);
            TempData["Updated"] = "Product Updated Successfully";
            return RedirectToAction("Index");
        }







    }
}