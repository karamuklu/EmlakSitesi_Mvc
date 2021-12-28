using MVC_Emlak_Sitesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Emlak_Sitesi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        EMLAK_SITESIEntities ent = new EMLAK_SITESIEntities();
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var kullaniciAdi = form["kullaniciAdi"].ToString();
            var sifre = form["sifre"].ToString();
            var kullanici = ent.TBLKULLANICI.Where(i => i.Kullanici_Adi == kullaniciAdi && i.sifre == sifre).FirstOrDefault();

            if (kullanici != null)
            {
                Session["Kullanıcı"] = kullanici.Adı + " " + kullanici.Soyadı;
                return RedirectToAction("index", "EMLAK");
            }
            else
            {
                Session["Mesaj"] = "Kullanıcı adı veya şifre hatalı";
                RedirectToAction("Login", "Login");
            }
            return View(kullanici);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            var kullaniciAdi = form["kullaniciAdi"].ToString();
            var sifre = form["sifre"].ToString();

            var gelenVeri=ent.TBLKULLANICI.Where(i=>i.Kullanici_Adi==kullaniciAdi && i.sifre==sifre).Count();

            if (gelenVeri==0)
            {
                TBLKULLANICI yeniKullanici = new TBLKULLANICI();
                yeniKullanici.Kullanici_Adi = form["KullaniciAdi"].ToString();
                yeniKullanici.sifre = form["sifre"].ToString();
                ent.TBLKULLANICI.Add(yeniKullanici);
                ent.SaveChanges();
                Session["Mesaj"] = "Kayıt başarıyla tamamlandı. Yönlendiriliyorsunuz...";
                System.Threading.Thread.Sleep(200);

                Response.Redirect("../Emlak/Index");
                
            }
            else if (gelenVeri>0)
            {
                Session["Mesaj"] = "Kullanıcı sistemde daha önceden kayıtlıdır. Lütfen Kullanıcı adınızı değiştiriniz";                
            }

            return View();
        }
        public ActionResult SignOut()
        {
            Session.Clear();
            return RedirectToAction("Index","Anasayfa");
        }
    }
}