using profosyonelsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace profosyonelsite.Controllers
{
	public class YazıController : Controller
	{
		detaylısiteEntities db = new detaylısiteEntities();
		[HttpGet]
		public ActionResult admin()
		{
			return View();
		}
		[HttpPost]
		public ActionResult admin(admin input, string username, string password)
		{
			var obj = db.admin.Where(m => m.Username == username && m.Password == password).FirstOrDefault();
			if (obj != null)
			{
				Session["adminid"] = obj.Userid;
				return RedirectToAction("blog");
			}
			else
			{
				return View();
			}
		}
		[HttpGet]
		public ActionResult blogolustur()
		{
			return View();
		}
		[HttpPost]
		public ActionResult blogolustur(blog input, HttpPostedFileBase image)
		{
			int id = Convert.ToInt32(Session["adminid"]);
			if (image != null && image.ContentLength > 0)
			{
				// Resmi kaydetmek için klasör yolu
				var folderPath = "/resimler/";
				var fileName = Path.GetFileName(image.FileName);  // Dosya adını al
				var filePath = Path.Combine(Server.MapPath("~" + folderPath), fileName);

				// Resmi belirtilen klasöre kaydet
				image.SaveAs(filePath);

				// Resmin URL'sini modelin ImageUrl alanına ekle
				input.img = folderPath + fileName;
			}
			input.userid = id;
			db.blog.Add(input);
			db.SaveChanges();
			return View();
		}
		[HttpGet]
		public ActionResult blogdetay(int blogid)
		{
			var list = db.blog.Include(m => m.yazı).FirstOrDefault(m => m.BlogID == blogid);
			return View(list);
		}
		[HttpPost]
		public ActionResult YazıEkle(int blogid, string header, string title, HttpPostedFileBase resim)
		{
			var blog = db.blog.Include(b => b.yazı).FirstOrDefault(b => b.BlogID == blogid);
			if (blog == null)
			{
				return HttpNotFound();
			}

			// Resim eklemek istiyorsanız, resmin kaydedilmesi işlemini burada yapabilirsiniz
			string resimUrl = "";
			if (resim != null && resim.ContentLength > 0)
			{
				var fileName = Path.GetFileName(resim.FileName);
				var filePath = Path.Combine(Server.MapPath("~/resimler/"), fileName);
				resim.SaveAs(filePath);
				resimUrl = "/resimler//" + fileName;
			}

			yazı yazılar = new yazı();
			{
				yazılar.header = header;
				yazılar.title = title;
				yazılar.resim = resimUrl;
				yazılar.blogid = blog.BlogID;
			}

			db.yazı.Add(yazılar);
			db.SaveChanges();  // Veritabanına kaydediyoruz

			// Sayfayı yeniden yüklüyoruz (eklenen yazıyı görmek için)
			return RedirectToAction("blogdetay", new { blogid = blogid });
		}
		public ActionResult blog()
		{
			var list = db.blog.ToList();
			return View(list);
		}
		public ActionResult yazısil(int yazıid)
		{
			var yaziıd = db.yazı.Where(m => m.yazıID == yazıid).FirstOrDefault();
			db.yazı.Remove(yaziıd);
			db.SaveChanges();
			return RedirectToAction("blog", "Yazı");
		}
	}
}