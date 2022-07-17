using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class TintucController : Controller
    {
        // GET: Tintuc


        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }

        private List<TinTuc> LayTT(int count)
        {
            return data.TinTucs.Where(  a => a.tintuc1 == ("NA")).Take(count).ToList();
        }
        public ActionResult TT()
        {
            var TT = LayTT(9);
            return View(TT);
        }

        public ActionResult CTTT(int id)
        {
            var TT = from s in data.TinTucs
                       where s.MaTT == id
                       select s;
            return View(TT.Single());
        }

    }
}