using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KitapListesiProje.Controllers
{
    public class HomeController : Controller
    {
        public KitaplarEntities db = new KitaplarEntities();
        public string publickullaniciAdi = "User";
        public string publicsifre = "Pass";
        
        public ActionResult Giris()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Giris(FormCollection fc)
        {
            string kullaniciAdi = fc["kullaniciAdi"];
            string sifre = fc["sifre"];

            if (kullaniciAdi==publickullaniciAdi && sifre==publicsifre)
            {
                Session["Yetki"] = kullaniciAdi;
                return Redirect("/Home/Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Index()
        {
            if (Session["Yetki"]!=null)
            {
                List<KitapListesi> kitaplar = db.KitapListesis.ToList();
                return View(kitaplar);
            }
            else
            {
                return Redirect("/Home/Giris");
            }
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(FormCollection fc)
        {
            if (Session["Yetki"] != null)
            {
                string kitapAdi = fc["kitapAdi"];
                string yazar = fc["yazar"];
                string fiyat = fc["fiyat"];
                string tarih = fc["tarih"];

                KitapListesi yeniKitap = new KitapListesi
                {
                    KitapAdi = kitapAdi,
                    Yazar = yazar,
                    Fiyat = Convert.ToDecimal(fiyat),
                    Tarih = Convert.ToDateTime(tarih)
                };
                db.KitapListesis.Add(yeniKitap);
                db.SaveChanges();
            }
                return Redirect("/Home/Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}