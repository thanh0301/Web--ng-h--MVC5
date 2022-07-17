using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web.Models;
namespace web.Models
{
    public class Giohang

    {
        DataClasses1DataContext data = new DataClasses1DataContext();

        public int iSanP { set; get; }
        public string sTenSanPham { set; get; }
        public string sAnh { set; get; }

        public Double dDongia {set; get;}

        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public Giohang(int MaSP)
        {
            iSanP = MaSP;
            SanPham sp = data.SanPhams.Single(n => n.MaSP == iSanP);
            sTenSanPham = sp.TenSP;
            sAnh = sp.Anh;
            dDongia  = double.Parse(sp.GiaBan.ToString());
            iSoluong = 1;
        }
}
}