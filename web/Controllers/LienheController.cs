using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class LienheController : Controller
    {
        // GET: Lienhe
        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Lienhe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Lienhe(FormCollection collection, LienHe lh)
        {
           
            var tieude = collection["Tieude"];
            var nd = collection["Noidung"];
          
           
            if (String.IsNullOrEmpty(tieude))
            {
                ViewData["loi1"] = "Vui lòng nhập tiêu đề";
            }
            else if (String.IsNullOrEmpty(nd))
            {
                ViewData["loi2"] = "Vui lòng nhập nội dung";
            }
               
            else
            {
                
                lh.TieuDe = tieude;
                lh.NoiDung = nd;
                
               
                data.LienHes.InsertOnSubmit(lh);
                data.SubmitChanges();
                
            }
            return this.Lienhe();
        }

       
    }
}