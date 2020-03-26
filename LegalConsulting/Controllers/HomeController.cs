using LegalConsulting.DAL;
using LegalConsulting.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LegalConsulting.Controllers
{
    public class HomeController : Controller

    {
        private ConsultingContext db = new ConsultingContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<HiringDateGroup> data = from lawyer in db.Lawyers
                                               group lawyer by lawyer.HiringDate into dateGroup
                                               select new HiringDateGroup()
                                               {
                                                   HiringDate = dateGroup.Key,
                                                   LawyerCount = dateGroup.Count()
                                               };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}