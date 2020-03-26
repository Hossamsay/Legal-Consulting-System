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

namespace LegalConsulting.Controllers
{
    public class CaseDetailController : Controller
    {
        private ConsultingContext db = new ConsultingContext();

        // GET: CaseDetail
        public async Task<ActionResult> Index()
        {
            var caseDetails = db.CaseDetails.Include(c => c.Case).Include(c => c.Client).Include(c => c.Lawyer);
            return View(await caseDetails.ToListAsync());
        }

        // GET: CaseDetail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseDetail caseDetail = await db.CaseDetails.FindAsync(id);
            if (caseDetail == null)
            {
                return HttpNotFound();
            }
            return View(caseDetail);
        }

        // GET: CaseDetail/Create
        public ActionResult Create()
        {
            ViewBag.CaseID = new SelectList(db.Cases, "CaseID", "CaseName");
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FirstName");
            ViewBag.LawyerID = new SelectList(db.Lawyers, "LawyerID", "FirstName");
            return View();
        }

        // POST: CaseDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ClientID,CaseID,LawyerID,Hours")] CaseDetail caseDetail)
        {
            if (ModelState.IsValid)
            {
                db.CaseDetails.Add(caseDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CaseID = new SelectList(db.Cases, "CaseID", "CaseName", caseDetail.CaseID);
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FirstName", caseDetail.ClientID);
            ViewBag.LawyerID = new SelectList(db.Lawyers, "LawyerID", "FirstName", caseDetail.LawyerID);
            return View(caseDetail);
        }

        // GET: CaseDetail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseDetail caseDetail = await db.CaseDetails.FindAsync(id);
            if (caseDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CaseID = new SelectList(db.Cases, "CaseID", "CaseName", caseDetail.CaseID);
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FirstName", caseDetail.ClientID);
            ViewBag.LawyerID = new SelectList(db.Lawyers, "LawyerID", "FirstName", caseDetail.LawyerID);
            return View(caseDetail);
        }

        // POST: CaseDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ClientID,CaseID,LawyerID,Hours")] CaseDetail caseDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caseDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CaseID = new SelectList(db.Cases, "CaseID", "CaseName", caseDetail.CaseID);
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FirstName", caseDetail.ClientID);
            ViewBag.LawyerID = new SelectList(db.Lawyers, "LawyerID", "FirstName", caseDetail.LawyerID);
            return View(caseDetail);
        }

        // GET: CaseDetail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseDetail caseDetail = await db.CaseDetails.FindAsync(id);
            if (caseDetail == null)
            {
                return HttpNotFound();
            }
            return View(caseDetail);
        }

        // POST: CaseDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CaseDetail caseDetail = await db.CaseDetails.FindAsync(id);
            db.CaseDetails.Remove(caseDetail);
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
