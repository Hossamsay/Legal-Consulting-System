using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LegalConsulting.DAL;
using LegalConsulting.Models;
using PagedList;
using LegalConsulting.ViewModel;

namespace LegalConsulting.Controllers
{
    public class LawyerController : Controller
    {
        private ConsultingContext db = new ConsultingContext();

        // GET: Lawyer
        public async Task<ActionResult> Index(int? id, int? caseID)
        {
            var viewModel = new LawyerIndexData();
            viewModel.Lawyers = db.Lawyers
            .Include(i => i.Cases)
            .Include(i => i.OfficeLocation)
            .Include(i => i.Cases.Select(c => c.Lawyers))
            .OrderBy(i => i.LastName);
            if (id != null)
            {
                ViewBag.LawyerID = id.Value;
                viewModel.Cases = viewModel.Lawyers.Where(
                i => i.LawyerID == id.Value).Single().Cases;
            }
            if (caseID != null)
            {
                ViewBag.CaseID = caseID.Value;
                viewModel.CaseDetails = viewModel.Cases.Where(
                x => x.CaseID == caseID.Value).Single().CaseDetails;
            }
            return View(viewModel);

        }

        // GET: Lawyer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyer lawyer = await db.Lawyers.FindAsync(id);
            if (lawyer == null)
            {
                return HttpNotFound();
            }
            return View(lawyer);
        }

        // GET: Lawyer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lawyer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,FirstName,LastName,CasetDate,PricePerHour,HiringDate,OfficeLocation")] Lawyer lawyer)
        {
            if (ModelState.IsValid)
            {
                db.Lawyers.Add(lawyer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lawyer);
        }

        // GET: Lawyer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyer lawyer = await db.Lawyers.FindAsync(id);
            if (lawyer == null)
            {
                return HttpNotFound();
            }
            return View(lawyer);
        }

        // POST: Lawyer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,LastName,CasetDate,PricePerHour,HiringDate,OfficeLocation")] Lawyer lawyer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lawyer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lawyer);
        }

        // GET: Lawyer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lawyer lawyer = await db.Lawyers.FindAsync(id);
            if (lawyer == null)
            {
                return HttpNotFound();
            }
            return View(lawyer);
        }

        // POST: Lawyer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lawyer lawyer = await db.Lawyers.FindAsync(id);
            db.Lawyers.Remove(lawyer);
            await db.SaveChangesAsync();
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
