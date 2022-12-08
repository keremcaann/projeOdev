using projeOdev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projeOdev.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ogrenciList()
        {
            var context = new MyContext();
            var ogrenci = context.Ogrencis.ToList();
            return View(ogrenci);
        }

        [HttpGet]
        public ActionResult ogrenciAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ogrenciAdd(Ogrenci ogrenci)
        {
            var context = new MyContext();
            if (!ModelState.IsValid)
            {
                return View("ogrenciAdd");
            }
            UpdateModel(ogrenci);
            context.Ogrencis.Add(ogrenci);
            context.SaveChanges();
            return RedirectToAction("ogrenciList", "Home");
        }

        [HttpGet]
        public ActionResult ogrenciEdit(int? id)
        {
            var context = new MyContext();
            var model = context.Ogrencis.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult ogrenciEdit(int? id, Ogrenci ogrenci)
        {
            var context = new MyContext();

            var model = context.Ogrencis.Find(id);
            model.AdSoyad = ogrenci.AdSoyad;
            UpdateModel(model);
            context.SaveChanges();
            return RedirectToAction("ogrenciList", "Home");

        }

        public ActionResult ogrenciRemove(int? id)
        {
            var context = new MyContext();
            var model = context.Ogrencis.SingleOrDefault(x => x.OgrenciId == id);
            context.Ogrencis.Remove(model);
            context.SaveChanges();
            return RedirectToAction("ogrenciList", "Home");
        }

        public ActionResult yonetimList()
        {
            var context = new MyContext();
            var okulYonetim = context.OkulYonetims.ToList();
            return View(okulYonetim);
        }

        [HttpGet]
        public ActionResult yonetimAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult yonetimAdd(Yonetim okulYonetim)
        {
            var context = new MyContext();
            if (!ModelState.IsValid)
            {
                return View("yonetimAdd");
            }
            UpdateModel(okulYonetim);
            context.OkulYonetims.Add(okulYonetim);
            context.SaveChanges();
            return RedirectToAction("yonetimList", "Home");
        }

        [HttpGet]
        public ActionResult yonetimEdit(int? id)
        {
            var context = new MyContext();
            var model = context.OkulYonetims.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult yonetimEdit(int? id,Yonetim okulYonetim)
        {
            var context = new MyContext();

            var model = context.OkulYonetims.Find(id);
            model.AdSoyad = okulYonetim.AdSoyad;
            UpdateModel(model);
            context.SaveChanges();
            return RedirectToAction("yonetimList", "Home");

        }

        public ActionResult yonetimRemove(int? id)
        {
            var context = new MyContext();
            var model = context.OkulYonetims.SingleOrDefault(x => x.YonetimId == id);
            context.OkulYonetims.Remove(model);
            context.SaveChanges();
            return RedirectToAction("yonetimList", "Home");
        }

        public ActionResult dersList()
        {
            var context = new MyContext();
            var ders = context.Derss.ToList();
            return View(ders);
        }

        [HttpGet]
        public ActionResult dersAdd(Ders ders)
        {
            using (var context = new MyContext())
            {
                List<Yonetim> ogretmenler = context.OkulYonetims.ToList();
                ViewBag.DDLOgretmenler = new SelectList(ogretmenler, "YonetimId", "AdSoyad");
                return View();
            }

        }


        [HttpPost]
        public ActionResult dersAdd(FormCollection formData, HttpPostedFileBase file)
        {
            using (var context = new MyContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        string ogretmenId = formData["DDLOgretmenler"].ToString();
                        var ders = new Ders();
                        ders.YonetimId = int.Parse(ogretmenId);
                        UpdateModel(ders);
                        context.Derss.Add(ders);
                        context.SaveChanges();
                        return RedirectToAction("dersList", "Home");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return View();
            }

        }

        [HttpGet]
        public ActionResult dersEdit(int? id)
        {
            var context = new MyContext();
            var model = context.Derss.Find(id);
            List<SelectListItem> ogretmenler = context.OkulYonetims.Select

                (
                x => new SelectListItem
                {
                    Text = x.AdSoyad,
                    Value = x.YonetimId.ToString(),
                    Selected = x.YonetimId == x.YonetimId
                }

                ).ToList();
            ViewBag.DDLOgretmenler = ogretmenler;
            return View(model);
        }

        [HttpPost]
        public ActionResult dersEdit(int? id, Ders ders)
        {
            var context = new MyContext();
            var model = context.Derss.Find(id);
            model.YonetimId = ders.YonetimId;
            UpdateModel(model);
            context.SaveChanges();
            return RedirectToAction("dersList", "Home");
        }

        public ActionResult dersRemove(int? id)
        {
            var context = new MyContext();
            var model = context.Derss.SingleOrDefault(x => x.DersId == id);
            context.Derss.Remove(model);
            context.SaveChanges();
            return RedirectToAction("dersList", "Home");
        }


        public ActionResult atamaList()
        {
            var context = new MyContext();
            var ogrenciDers = context.Atamas.ToList();
            return View(ogrenciDers);
        }

        [HttpGet]
        public ActionResult atamaAdd()
        {
            using (var context = new MyContext())
            {
                List<Ogrenci> ogrenciler = context.Ogrencis.ToList();
                ViewBag.DDLOgrenciler = new SelectList(ogrenciler, "OgrenciId", "AdSoyad");

                List<Ders> dersler = context.Derss.ToList();
                ViewBag.DDLDersler = new SelectList(dersler, "DersId", "DersAdi");
                return View();
            }

        }


        [HttpPost]
        public ActionResult atamaAdd(FormCollection formData)
        {
            using (var context = new MyContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        string ogrenciId = formData["DDLOgrenciler"].ToString();
                        string dersId = formData["DDLDersler"].ToString();
                        var ogrenciDers = new Atama();
                        ogrenciDers.OgrenciId = int.Parse(ogrenciId);
                        ogrenciDers.DersId = int.Parse(dersId);
                        UpdateModel(ogrenciDers);
                        context.Atamas.Add(ogrenciDers);
                        context.SaveChanges();
                        return RedirectToAction("atamaList", "Home");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return View();
            }

        }
        [HttpGet]
        public ActionResult atamaEdit(int? id)
        {
            var context = new MyContext();
            var model = context.Derss.Find(id);
            List<SelectListItem> dersler = context.Derss.Select

                (
                x => new SelectListItem
                {
                    Text = x.DersAdi,
                    Value = x.DersId.ToString(),
                    Selected = x.DersId == x.DersId
                }

                ).ToList();
            ViewBag.DDLDersler = dersler;

            List<SelectListItem> ogrenciler = context.Ogrencis.Select

               (
               x => new SelectListItem
               {
                   Text = x.AdSoyad,
                   Value = x.OgrenciId.ToString(),
                   Selected = x.OgrenciId == x.OgrenciId
               }

               ).ToList();
            ViewBag.DDLOgrenciler = ogrenciler;
            return View(model);
        }

        [HttpPost]
        public ActionResult atamaEdit(int? id, Atama ogrenciDers)
        {
            var context = new MyContext();
            var model = context.Atamas.Find(id);
            model.DersId = ogrenciDers.DersId;
            model.OgrenciId = ogrenciDers.OgrenciId;
            UpdateModel(model);
            context.SaveChanges();
            return RedirectToAction("atamaList", "Home");
        }

        public ActionResult atamaRemove(int? id)
        {
            var context = new MyContext();
            var model = context.Atamas.SingleOrDefault(x => x.OgrenciDersId == id);
            context.Atamas.Remove(model);
            context.SaveChanges();
            return RedirectToAction("atamaList", "Home");
        }
    }
}