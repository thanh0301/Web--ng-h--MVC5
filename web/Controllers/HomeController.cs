using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models;
using PagedList;
using PagedList.Mvc;
namespace web.Controllers
{
    public class HomeController : Controller
    {

        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Trangchu()
        {
            var DHNam = LayDHNam3(6);
            return View(DHNam);
        }

            public ActionResult Index()
        {
            return View();
        }

      

        private List<SanPham> LayDHNam3(int count)
        {

            return data.SanPhams.Where(a => a.MaLoaiSP == (3)).Take(count).ToList();
        }
       


        private List<SanPham> LayDHNam(int count)
        {
            return data.SanPhams.Where(a => a.GioiTinh == ("Nam")).Take(count).ToList();
        }

        public ActionResult DHNam(int ? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);

            var dhnam = LayDHNam(20);
            return View(dhnam.ToPagedList(pageNum, pageSize));
        }

        private List<SanPham> LayDHNu(int count)
        {
            return data.SanPhams.Where(a => a.GioiTinh == ("Nữ")).Take(count).ToList();
        }
        public ActionResult DHNu(int? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);

            var dhnu = LayDHNu(20);
            return View(dhnu.ToPagedList(pageNum, pageSize));
        }
        private List<SanPham> LayDHCASIO(int count)
        {
          
            return data.SanPhams.Where(a => a.MaLoaiSP == (1)).Take(count).ToList();
        }
        public ActionResult CASIO()
        {
            var CASIO = LayDHCASIO(9);
            return View(CASIO);
        }
        private List<SanPham> LayDHDOXA(int count)
        {

            return data.SanPhams.Where(a => a.MaLoaiSP == (5)).Take(count).ToList();
        }
        public ActionResult DOXA()
        {
            var DOXA = LayDHDOXA(9);
            return View(DOXA);
        }

        private List<SanPham> LayDHSEIKO(int count)
        {

            return data.SanPhams.Where(a => a.MaLoaiSP == (8)).Take(count).ToList();
        }
        public ActionResult SEIKO()
        {
            var SEIKO = LayDHSEIKO(9);
            return View(SEIKO);
        }
        private List<SanPham> LayDHNam2(int count)
        {

            return data.SanPhams.Where(a => a.MaLoaiSP == (2)).Take(count).ToList();
        }
        public ActionResult LayDHNam2()
        {
            var DHNam = LayDHNam2(3);
            return View(DHNam);
        }
        private List<SanPham> LayDHNu2(int count)
        {

            return data.SanPhams.Where(a => a.MaLoaiSP == (3)).Take(count).ToList();
        }
        public ActionResult LayDHNu2()
        {
            var DHNu = LayDHNu2(9);
            return View(DHNu);
        }
        public ActionResult Dedails(int id)
        {
            var DHNam = from s in data.SanPhams
                         where s.MaSP == id
                         select s;
            return View(DHNam.Single());
        }
        public ActionResult CTSPN(int id)
        {
            var DHNu = from s in data.SanPhams
                        where s.MaSP == id
                        select s;
            return View(DHNu.Single());
        }
        public ActionResult CTSPN1(int id)
        {
            var DHNu1 = from s in data.SanPhams
                       where s.MaSP == id
                       select s;
            return View(DHNu1.Single());
        }
        public ActionResult CTSPNAM(int id)
        {
            var DHNamu2 = from s in data.SanPhams
                       where s.MaSP == id
                       select s;
            return View(DHNamu2.Single());
        }
        public ActionResult CTSPCASIO(int id)
        {
            var DHCasio = from s in data.SanPhams
                          where s.MaSP == id
                          select s;
            return View(DHCasio.Single());
        }
        public ActionResult CTSPDOXA(int id)
        {
            var DHDoxa = from s in data.SanPhams
                          where s.MaSP == id
                          select s;
            return View(DHDoxa.Single());
        }
        public ActionResult CTSPSEIKO(int id)
        {
            var DHSeiko = from s in data.SanPhams
                          where s.MaSP == id
                          select s;
            return View(DHSeiko.Single());
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