using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Emlak_Sitesi.Models;

namespace MVC_Emlak_Sitesi.Controllers
{
    public class EMLAKController : Controller
    {
        private EMLAK_SITESIEntities db = new EMLAK_SITESIEntities();

        // GET: EMLAK
        public ActionResult Index()
        {
            
            var tBLEMLAK = db.TBLEMLAK.Where(i => i.isDeleted == false).Include(t => t.TBLSORUMLU).Include(t => t.TBLEMLAK_DETAY).Include(t => t.TBLEMLAKTIP);

            if (Session["Kullanici"] != null)
            {                
                return View(tBLEMLAK.ToList());
            }
            else
            {
                RedirectToAction("Login", "Login");
                return View(tBLEMLAK.ToList());   
            }
        }

        // GET: EMLAK/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLEMLAK tBLEMLAK = db.TBLEMLAK.Find(id);
            if (tBLEMLAK == null)
            {
                return HttpNotFound();
            }
            return View(tBLEMLAK);
        }

        // GET: EMLAK/Create
        public ActionResult Create()
        {
            ViewBag.SORUMLU_ID = new SelectList(db.TBLSORUMLU, "ID", "ADI");
            ViewBag.ILAN_NO = new SelectList(db.TBLEMLAK_DETAY, "ILAN__DETAY_NO", "CEPHE");
            ViewBag.EMLAK_TIP_ID = new SelectList(db.TBLEMLAKTIP, "ID", "Emlak_Tipi");
            return View();
        }

        // POST: EMLAK/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ILAN_NO,LAT,LNG,ILAN_ICERIGI,ILAN_BASLIGI,ILAN_TARIHI,SATILIK_KIRALIK,BINA_YASI,BULUNDUGU_KAT,TOPLAM_KAT_SAYISI,ISITMA_TIPI,FIYAT,SORUMLU_ID,M2,ODA_SAYISI,KONUMU,ONE_CIKAN,EMLAK_TIP_ID,FIYATI_DUSEN,isDeleted")] TBLEMLAK tBLEMLAK)
        {
            if (ModelState.IsValid)
            {
                TBLRESIM yeniResim = new TBLRESIM();
                db.TBLEMLAK.Add(tBLEMLAK);
                yeniResim.SIRA_NO = 1;
                yeniResim.EMLAK_ILAN_NO = tBLEMLAK.ILAN_NO;
                yeniResim.ALT_ETIKETI = "No Image";
                yeniResim.RESIM_YOLU = "../../img/no_image.png";
                yeniResim.isDeleted = false;
                db.TBLRESIM.Add(yeniResim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SORUMLU_ID = new SelectList(db.TBLSORUMLU, "ID", "ADI"+" "+ "SOYADI", tBLEMLAK.SORUMLU_ID);
            ViewBag.ILAN_NO = new SelectList(db.TBLEMLAK_DETAY, "ILAN__DETAY_NO", "CEPHE", tBLEMLAK.ILAN_NO);
            ViewBag.EMLAK_TIP_ID = new SelectList(db.TBLEMLAKTIP, "ID", "Emlak_Tipi", tBLEMLAK.EMLAK_TIP_ID);
            return View(tBLEMLAK);
        }

        // GET: EMLAK/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLEMLAK tBLEMLAK = db.TBLEMLAK.Find(id);
            if (tBLEMLAK == null)
            {
                return HttpNotFound();
            }
            ViewBag.SORUMLU_ID = new SelectList(db.TBLSORUMLU, "ID", "ADI", tBLEMLAK.SORUMLU_ID);
           // ViewBag.ILAN_NO = new SelectList(db.TBLEMLAK_DETAY, "ILAN__DETAY_NO", "CEPHE", tBLEMLAK.ILAN_NO);
            ViewBag.EMLAK_TIP_ID = new SelectList(db.TBLEMLAKTIP, "ID", "Emlak_Tipi", tBLEMLAK.EMLAK_TIP_ID);
            return View(tBLEMLAK);
        }

        // POST: EMLAK/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ILAN_NO,LAT,LNG,ILAN_ICERIGI,ILAN_BASLIGI,ILAN_TARIHI,SATILIK_KIRALIK,BINA_YASI,BULUNDUGU_KAT,TOPLAM_KAT_SAYISI,ISITMA_TIPI,FIYAT,SORUMLU_ID,M2,ODA_SAYISI,KONUMU,ONE_CIKAN,EMLAK_TIP_ID,FIYATI_DUSEN")] TBLEMLAK tBLEMLAK)
        {
            if (ModelState.IsValid)
            {
                tBLEMLAK.isDeleted = false;
                db.Entry(tBLEMLAK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SORUMLU_ID = new SelectList(db.TBLSORUMLU, "ID", "ADI SOYADI", tBLEMLAK.SORUMLU_ID);
            ViewBag.ILAN_NO = new SelectList(db.TBLEMLAK_DETAY, "ILAN__DETAY_NO", "CEPHE", tBLEMLAK.ILAN_NO);
            ViewBag.EMLAK_TIP_ID = new SelectList(db.TBLEMLAKTIP, "ID", "Emlak_Tipi", tBLEMLAK.EMLAK_TIP_ID);
            return View(tBLEMLAK);
        }

        // GET: EMLAK/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBLEMLAK tBLEMLAK = db.TBLEMLAK.Find(id);
            if (tBLEMLAK == null)
            {
                return HttpNotFound();
            }
            return View(tBLEMLAK);
        }

        // POST: EMLAK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBLEMLAK tBLEMLAK = db.TBLEMLAK.Find(id);
            tBLEMLAK.isDeleted = true;
            //db.TBLEMLAK.Remove(tBLEMLAK);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
