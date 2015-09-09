using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WMS.Models;
using PagedList;
using System.IO;
using System.Web.Helpers;
using WMS.Controllers.Filters;
using WMS.HelperClass;
using WMS.CustomClass;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
namespace WMS.Controllers
{
    
    public class AttProcessorsController : Controller
    {
        private TAS2013Entities context = new TAS2013Entities();

        //
        // GET: /AttProcessors/

        public ViewResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TagSortParm = String.IsNullOrEmpty(sortOrder) ? "tag_desc" : "";
            ViewBag.FromSortParm = sortOrder == "from" ? "from_desc" : "from";
            ViewBag.ToSortParm = sortOrder == "to" ? "to_desc" : "to";
            ViewBag.WhenToSortParm = sortOrder == "whento" ? "whento_desc" : "whento";
            ViewBag.LocationSortParm = sortOrder == "location" ? "location_desc" : "location";
            ViewBag.CompanySortParm = sortOrder == "company" ? "company_desc" : "company";
            ViewBag.CatSortParm = sortOrder == "cat" ? "cat_desc" : "cat";

            if (searchString != null)
                page = 1;
             else
               searchString = currentFilter;

            List<AttProcessorScheduler> attprocess = context.AttProcessorSchedulers.ToList();
            switch (sortOrder)
            {
                case "tag_desc": attprocess = attprocess.OrderByDescending(s => s.PeriodTag).ToList();                   break;
                case "from_desc":
                    attprocess = attprocess.OrderByDescending(s => s.DateFrom).ToList();
                    break;
                case "from":
                   attprocess = attprocess.OrderBy(s => s.DateFrom).ToList();
                    break;
                case "to_desc":
                    attprocess = attprocess.OrderByDescending(s => s.DateTo).ToList();
                    break;
                case "to":
                    attprocess = attprocess.OrderBy(s => s.DateTo).ToList();
                    break;
                case "whento_desc":
                    attprocess = attprocess.OrderByDescending(s => s.WhenToProcess).ToList();
                    break;
                case "whento":
                    attprocess = attprocess.OrderBy(s => s.WhenToProcess).ToList();
                    break;
               
                default:
                    attprocess = attprocess.OrderBy(s => s.PeriodTag).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(attprocess.ToPagedList(pageNumber, pageSize));
           
        }

        //
        // GET: /AttProcessors/Details/5

        public ViewResult Details(int id)
        {
            AttProcessorScheduler attprocessor = context.AttProcessorSchedulers.Single(x => x.AttProcesserID == id);
            return View(attprocessor);
        }

        //
        // GET: /AttProcessors/Create

        public ActionResult Create()
        {
            TAS2013Entities db = new TAS2013Entities();
            User LoggedInUser = Session["LoggedUser"] as User;
            QueryBuilder qb = new QueryBuilder();
            String query = qb.QueryForCompanyViewLinq(LoggedInUser);
            ViewBag.PeriodTag = new SelectList(new List<SelectListItem>
{
    new SelectListItem { Selected = true, Text = "Daily", Value = "D"},
    new SelectListItem { Selected = false, Text = "Monthly", Value = "M"},
    new SelectListItem { Selected = false, Text = "Summary", Value = "S"},

}, "Value" , "Text",1);
            ViewBag.CompanyID = new SelectList(db.Companies.Where(query), "CompID", "CompName");
            query = qb.QueryForLocationTableSegerationForLinq(LoggedInUser);
            ViewBag.LocationID = new SelectList(db.Locations.Where(query), "LocID", "LocName");
            
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName");
             return View();
        } 

        //
        // POST: /AttProcessors/Create

        [HttpPost]
        public ActionResult Create(AttProcessorScheduler attprocessor)
        {
            if (ModelState.IsValid)
            {
                context.AttProcessorSchedulers.Add(attprocessor);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(attprocessor);
        }
        
        //
        // GET: /AttProcessors/Edit/5
 
        public ActionResult Edit(int id)
        {
            AttProcessorScheduler attprocessor = context.AttProcessorSchedulers.Single(x => x.AttProcesserID == id);
            return View(attprocessor);
        }

        //
        // POST: /AttProcessors/Edit/5

        [HttpPost]
        public ActionResult Edit(AttProcessorScheduler attprocessor)
        {
            if (ModelState.IsValid)
            {
                context.Entry(attprocessor).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attprocessor);
        }

        //
        // GET: /AttProcessors/Delete/5
 
        public ActionResult Delete(int id)
        {
            AttProcessorScheduler attprocessor = context.AttProcessorSchedulers.Single(x => x.AttProcesserID == id);
            return View(attprocessor);
        }

        //
        // POST: /AttProcessors/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AttProcessorScheduler attprocessor = context.AttProcessorSchedulers.Single(x => x.AttProcesserID == id);
            context.AttProcessorSchedulers.Remove(attprocessor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}