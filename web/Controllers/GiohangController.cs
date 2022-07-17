using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models;

namespace web.Controllers
{
    public class GiohangController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: Giohang
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGiohang(int iSanP, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();

            Giohang sanpham = lstGiohang.Find(n => n.iSanP == iSanP);
            if (sanpham == null)
            {
                sanpham = new Giohang(iSanP);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                    sanpham.iSoluong++;
                 return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if(lstGiohang!=null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        public ActionResult Giohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count ==0)
            {
                return RedirectToAction("Trangchu", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGiohang) ;
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGiohang( int iMaSp)
        {
            List<Giohang> lstGiohang = Laygiohang();

            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iSanP == iMaSp);

            if(sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iSanP == iMaSp);
                return RedirectToAction("Giohang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Trangchu", "Home");
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult CapnhatGioHang(int iMaSp,FormCollection f)
        {
            List<Giohang> lstGiohang = Laygiohang();

            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iSanP == iMaSp);

            if(sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }

        public ActionResult XoaTatcaGiohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Trangchu", "Home");
        }
        public ActionResult DatHang()
        {
            if(Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Trangchu", "Home");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult DatHang( FormCollection collection)
        {
            DatHang dh = new DatHang();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            dh.MaKH = kh.MaKH;
         /*   dh.NgayDat = DateTime.Now;
            var ngaygiao = String.Format("0:MM/dd/yyyy", collection["Ngaygiao"]);
            dh.Ngaygiao = DateTime.Parse(ngaygiao);*/
            dh.Tinhtranggiaohang = false;
            dh.Dathanhtoan = false;
            data.DatHangs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach(var item in gh)
            {
                CTDatHang ctdh = new CTDatHang();
                ctdh.MaDH = dh.MaDH;
                ctdh.MaSP = item.iSanP;
                ctdh.soluong = item.iSoluong;
                ctdh.Dongia = (decimal)item.dDongia;
                data.CTDatHangs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }


    }
 }
