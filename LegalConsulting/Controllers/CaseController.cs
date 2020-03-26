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
using LegalConsulting.ViewModel;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace LegalConsulting.Controllers
{
    public class CaseController : Controller
    {
        private ConsultingContext db = new ConsultingContext();

        // GET: Case
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var cases = from s in db.Cases
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                cases = cases.Where(s => s.Description.ToUpper().Contains(searchString.ToUpper())
                  || s.CaseName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    cases = cases.OrderByDescending(s => s.CaseName);
                    break;
                case "Date":
                    cases = cases.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    cases = cases.OrderByDescending(s => s.StartDate);
                    break;
                default:
                    cases = cases.OrderBy(s => s.CaseName);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(cases.ToPagedList(pageNumber, pageSize));


        }

        // GET: Case/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case cas = await db.Cases.FindAsync(id);
            if (cas == null)
            {
                return HttpNotFound();
            }
            return View(cas);

        }

        // GET: Case/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Case/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CaseID,ClientID,Type,Description,CaseName,StartDate")] Case cas)
        {
            if (ModelState.IsValid)
            {
                db.Cases.Add(cas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cas);
        }

        // GET: Case/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case cas = await db.Cases.FindAsync(id);
            if (cas == null)
            {
                return HttpNotFound();
            }
            return View(cas);
        }

        // POST: Case/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CaseID,ClientID,Type,Description,CaseName,StartDate")] Case cas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cas).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (Case)entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes.The Case was deleted by another user.");
                }
                else
                {
                    var databaseValues = (Case)databaseEntry.ToObject();    
                    if (databaseValues.ClientID != clientValues.ClientID)
                        ModelState.AddModelError("ClientID", "Currentvalue:" + databaseValues.ClientID);
                    if (databaseValues.Description != clientValues.Description) ModelState.AddModelError("Description", "Currentvalue:"
                         + databaseValues.Description);
                    if (databaseValues.Type != clientValues.Type) ModelState.AddModelError("Type", "Currentvalue:" + databaseValues.Type);
                    ModelState.AddModelError(string.Empty, "The record you attempted to edit"
                    + "was modified by another user after you got the original value.The"
                    + "edit operation was canceled and the current values in the database"
                    + "have been displayed.If you still want to edit this record,click"
                    + "the Save button again.Other wise click the Back to List hyperlink.");
                    cas.RowVersion = databaseValues.RowVersion;
                }
            }
            catch (RetryLimitExceededException/*dex*/)
            {

                ModelState.AddModelError(string.Empty, "Unable to save changes.Tryagain,and if the problem persists contact your system administrator.");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FullName",cas.ClientID);
            return View(cas);


        }

        // GET: Case/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case cas = await db.Cases.FindAsync(id);
            if (cas == null)
            {

                if (concurrencyError == true)
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }
            if (concurrencyError.GetValueOrDefault())
            {
                if (cas == null)
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete"
                    + "was deleted by another user after you got the original values."
                    + "Click the Back to List hyper link.";
                }
                else
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete"
                    + "was modified by another user after you got the original values."
                    + "The delete operation was canceled and the current values in the"
                    + "database have been displayed.If you still want to delete this"
                    + "record,click the Delete button again.Otherwise"
                    + "click the Back to List hyperlink.";
                }
            }
            return View(cas);
        }

        // POST: Case/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Case cas)
        {
            try
            {
                db.Entry(cas).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = cas.CaseID
                });
            }
            catch (DataException/*dex*/)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete.Tryagain,and if the problem persists contact your system administrator.");
                return View(cas);
            }

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
