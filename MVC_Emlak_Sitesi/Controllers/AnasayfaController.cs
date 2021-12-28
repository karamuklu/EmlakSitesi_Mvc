using MVC_Emlak_Sitesi.Models;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace MVC_Emlak_Sitesi.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        EMLAK_SITESIEntities ent = new EMLAK_SITESIEntities();
        public ActionResult Index()
        {

            ViewBag.ilan_No = new SelectList(ent.TBLEMLAK, "ILAN_NO", "KONUMU");

            return View();
        }
        public ActionResult EmlakDetay(int? id)
        {
            if (id == null)
            {
                Response.Redirect("~/Anasayfa/HataSayfasi");
            }
            var secilenEmlak = ent.TBLEMLAK.Find(id);

            return View(secilenEmlak);
        }
        public JsonResult EmlakKonumGetir(int? id)
        {
            var emlakKonumu = ent.TBLEMLAK.Where(i => i.ILAN_NO == id).Select(i => new Harita_Veri()
            {
                ILAN_NO = i.ILAN_NO,
                ILAN_BASLIGI = i.ILAN_BASLIGI,
                ILAN_ICERIGI = i.ILAN_ICERIGI,
                LAT = i.LAT,
                LNG = i.LNG,
                RESIM = "../../img/marker_3.png"
            });
            return Json(emlakKonumu, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmlakListesi(int sayfa = 1)
        {
            var emlaklar = ent.TBLEMLAK.OrderBy(i => i.ILAN_NO).ToPagedList(sayfa, 4);
            return View(emlaklar);
        }

        public ActionResult EmlakciDetay(int? id)
        {
            EMLAKCI_DETAY emlakci = new EMLAKCI_DETAY();
            emlakci.EMLAKCI = ent.TBLSORUMLU.Find(id);
            emlakci.IlgilendigiEmlaklar = ent.TBLEMLAK.Where(i => i.SORUMLU_ID == id).ToList();
            return View(emlakci);
        }
        public JsonResult YerleriGetir()
        {
            var yerler = ent.TBLEMLAK.Where(i => i.isDeleted == false).Select(i => new Harita_Veri()
            {
                ILAN_NO = i.ILAN_NO,
                ILAN_BASLIGI = i.ILAN_BASLIGI,
                ILAN_ICERIGI = i.ILAN_ICERIGI,
                LAT = i.LAT,
                LNG = i.LNG,
                FIYAT = (int)i.FIYAT,
                ODA_SAYISI = i.ODA_SAYISI,
                SATILIK_KIRALIK = i.SATILIK_KIRALIK,
                M2 = (int)i.M2,
                KONUMU = i.KONUMU,
                RESIM = i.TBLRESIM.FirstOrDefault().RESIM_YOLU
            }).ToList();

            return Json(yerler, JsonRequestBehavior.AllowGet);
        }
        public JsonResult YerEtiketiGetir()
        {
            var emlakEtiketler = ent.TBLEMLAK.Where(i => i.isDeleted == false).ToList();
            return Json(emlakEtiketler, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MailGonder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MailGonder(FormCollection form, int id)
        {
            var isim = form["isim"].ToString();
            var telefon = form["telefon"].ToString();
            var tarih = form["tarih"].ToString();
            var mail = form["email"].ToString();
            var mesaj = form["mesaj"].ToString();

            if (MailAtar(isim, telefon, tarih, mail, mesaj))
            {
                TempData["Mesaj"] = "Mesajınız gönderilmiştir.";
            }
            else
            {
                TempData["Mesaj"] = "Gönderme başarısız";
            }

            return RedirectToAction("EmlakDetay/" + id);
        }
        public bool MailAtar(string isim, string telefon, string tarih, string gonderilen, string icerik)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("mustafakaramuklu@gmail.com");
            ePosta.To.Add(gonderilen);
            ePosta.Subject = "Siteden Mesaj Var";
            ePosta.Body = "Maili Gönderen :" + isim + " <br> Telefon :" + telefon + "<br> Gönderilme Tarihi : " + tarih + " <br> Mesaj İçeriği :" + icerik;

            ePosta.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("mustafakaramuklu@gmail.com", "20055858");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(ePosta);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public ActionResult EmlakAra(string aranan, int sayfa = 1)
        {
            if (aranan == "" || aranan == null)
            {
                Response.Redirect("Anasayfa/Index");
            }
            var arananEmlaklar = ent.TBLEMLAK.Where(i => i.ILAN_BASLIGI.ToLower().Contains(aranan.ToLower()) || i.ILAN_ICERIGI.ToLower().Contains(aranan.ToLower())).OrderBy(a => a.ILAN_NO).ToPagedList(sayfa, 4);
            return View(arananEmlaklar);
        }
        public JsonResult EmlakDetayGetir(int? id)
        {
            if (id == null)
            {
                Response.Redirect("~/Anasayfa/HataSayfasi");
            }
            var secilenEmlak = ent.TBLEMLAK_DETAY.Find(id);
            return Json(secilenEmlak, JsonRequestBehavior.AllowGet);
        }

        //string konumu, int ilkFiyat, int sonFiyat, int ilkmk, int sonmk
        [HttpPost]
        public ActionResult EmlakFiltrele(FormCollection form, int sayfa = 1)
        {
            var konumu = form["ilan_No"];
            int ilkFiyat = Convert.ToInt32(form["ilkFiyat"]);
            //int sonFiyat = Convert.ToInt32(form["sonFiyat"]);
            //var ilkmk = Convert.ToInt32(form["ilkmk"]);
            //var sonmk = Convert.ToInt32(form["sonmk"]);

            if (ilkFiyat == 0 )
            {
                Response.Redirect("Anasayfa/Iletisim");
            }
            var emlaklar=ent.TBLEMLAK.Where(i => i.FIYAT > ilkFiyat).OrderBy(a => a.ILAN_NO).ToPagedList(sayfa, 10);
            return View("EmlakListesi", emlaklar);
        }


        public ActionResult CokSorulanlar()
        {
            return View();
        }

        public ActionResult Iletisim()
        {
            ViewBag.ilan_No = new SelectList(ent.TBLEMLAK, "ILAN_NO", "KONUMU");
            return View();
        }
    }
}