using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoangadmin"] = ad;
                    return RedirectToAction("DH", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();

        }

        public ActionResult DH(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;

            return View(data.SanPhams.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));


        }

        [HttpGet]
        public ActionResult Themmoidh()
        {
            ViewBag.MaLoaiSP = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoidh(SanPham sanpham, HttpPostedFileBase fileuUpload)
        {
            ViewBag.MaLoaiSP = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");

            if (fileuUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileuUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileuUpload.SaveAs(path);
                    }
                    sanpham.Anh = fileName;

                    data.SanPhams.InsertOnSubmit(sanpham);
                    data.SubmitChanges();
                }
                return RedirectToAction("DH");
            }
        }

        public ActionResult chitietdh(int id)
        {
            SanPham sanpham = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }


        public ActionResult xoadh(int id)
        {
            SanPham sanpham = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sanpham.MaSP;
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        [HttpPost, ActionName("xoadh")]

        public ActionResult Xacnhanxoa(int id)
        {
            SanPham sanpham = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SanPhams.DeleteOnSubmit(sanpham);
            data.SubmitChanges();
            return RedirectToAction("DH");


        }

        public ActionResult suadh(int id)
        {
            /*SanPham sanpham = data.SanPhams.SingleOrDefault(n => n.MaSP == id);

            if(sanpham==null)
            {
                Response.StatusCode = 404;
                return null;


            }
            ViewBag.MaLoaiSP = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP",sanpham.MaLoaiSP);
            return View(sanpham);*/
            SanPham sanpham = data.SanPhams.SingleOrDefault(n => n.MaSP == id);

            //Lấy Ảnh



            //Đưa dữ liệu vào dropdownlist
            //Lấy ds từ table LoaiSanPham, sắp xếp tăng dần theo tên loại, chọn lấy giá trị MaLoai hien thi theo TenLoai
            ViewBag.MaLoai = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");


            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }

        [HttpPost, ValidateInput(false), ActionName("suadh")]
        public ActionResult xacnhansuadh(int id, HttpPostedFileBase fileupload)
        {

            /* ModelState.Clear();
             ViewBag.MaLoaiSP = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");

             if(fileuUpload == null)
             {
                 ViewBag.Thongbao = "Vui long chon anh bia";
                 return View();
             }
             else
             {
                 if (ModelState.IsValid)
                 {

                     var fileName = Path.GetFileName(fileuUpload.FileName);
                     var path = Path.Combine(Server.MapPath("~/images"), fileName);

                     if (System.IO.File.Exists(path))
                     ViewBag.Thongbao = "Hinh anh da ton tai";
                     else
                     {
                         fileuUpload.SaveAs(path);
                     }
                     SanPham sanpham = data.SanPhams.SingleOrDefault(x => x.MaSP == id);

                     sanpham.Anh = fileName;
                     data.SubmitChanges();

                     UpdateModel(sanpham);

                 }
                 return RedirectToAction("DH");
             }*/
            //Lấy ra sp cần sửa
            SanPham sanPham = data.SanPhams.SingleOrDefault(n => n.MaSP == id);

            //Lấy ds từ table LoaiSanPham, sắp xếp tăng dần theo tên loại, chọn lấy giá trị MaLoai hien thi theo TenLoai
            ViewBag.MaLoai = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.TenLoaiSP), "MaLoaiSP", "TenLoaiSP");

            //Kiểm tra đường dẫn file
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Lưu tên file
                    var fileName = Path.GetFileName(fileupload.FileName);
                    //Lưu đường dẫn của file
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    //Kiểm tra hình ảnh tồn tại chưa
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Lưu hình ảnh vào đường dẫn
                        fileupload.SaveAs(path);
                    }
                    SanPham sanpham = data.SanPhams.SingleOrDefault(x => x.MaSP == id);
                    sanPham.Anh = fileName;
                  
                    UpdateModel(sanPham);
                    data.SubmitChanges();

                }

                return RedirectToAction("DH");
            }

        }
    }
}