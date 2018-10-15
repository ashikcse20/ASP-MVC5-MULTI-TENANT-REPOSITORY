using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AspMvc5MultiTenantProject.Models;

namespace AspMvc5MultiTenantProject.Controllers
{
    public class Test1Controller : Controller
    { 
		  private BaseModel baseModel = new BaseModel();
		  public ActionResult Index()
		  {
				return View();
		  }
		  public ActionResult TableData()
        {
				List<TBL_TEST_1> l = new List<TBL_TEST_1>();
				try
				{
					l = baseModel.Entity.TBL_TEST_1.ToList();

				}
				catch (Exception e)
				{
					 return Content(e.Message.ToString());
				}
				return View(l);
        }
		  public ActionResult TenantInfo()
		  {
				var mytenantDetails = this.RouteData.Values["tenant"].ToString();
				if(!(mytenantDetails == "This is the main domain"))
				{
					 mytenantDetails = "Tenant Name is :" + mytenantDetails;
				}
				return Content(mytenantDetails);
		  }
		  public ActionResult Report(string id )
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "Test1Report.rdlc");
            if(System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<TBL_TEST_1> tt = new List<TBL_TEST_1>();
            tt = baseModel.Entity.TBL_TEST_1.ToList();
            ReportDataSource rd = new ReportDataSource("DataSet1", tt);
            lr.DataSources.Add(rd);

            string reportType = id;
            string mimtype;
            string encoding;
            string FileNameExtension;

            Warning[] warning;
            string[] streams;
            byte[] renderBytes;
            renderBytes = lr.Render(reportType,
                "", out mimtype, out encoding, out FileNameExtension, out streams, out warning);
            return File(renderBytes, mimtype);
        }

        // GET: Test1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_TEST_1 tBL_TEST_1 = baseModel.Entity.TBL_TEST_1.Find(id);
            if (tBL_TEST_1 == null)
            {
                return HttpNotFound();
            }
            return View(tBL_TEST_1);
        }

        // GET: Test1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TBL_TEST_1 tBL_TEST_1)
        {
            if (ModelState.IsValid)
            {
                baseModel.Entity.TBL_TEST_1.Add(tBL_TEST_1);
                baseModel.Entity.SaveChanges();
                return RedirectToAction("TableData");
            }

            return View(tBL_TEST_1);
        }

        // GET: Test1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_TEST_1 tBL_TEST_1 = baseModel.Entity.TBL_TEST_1.Find(id);
            if (tBL_TEST_1 == null)
            {
                return HttpNotFound();
            }
            return View(tBL_TEST_1);
        }

        // POST: Test1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TBL_TEST_1 tBL_TEST_1)
        {
            if (ModelState.IsValid)
            {
                baseModel.Entity.Entry(tBL_TEST_1).State = EntityState.Modified;
                baseModel.Entity.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tBL_TEST_1);
        }

        // GET: Test1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBL_TEST_1 tBL_TEST_1 = baseModel.Entity.TBL_TEST_1.Find(id);
            if (tBL_TEST_1 == null)
            {
                return HttpNotFound();
            }
            return View(tBL_TEST_1);
        }

        // POST: Test1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TBL_TEST_1 tBL_TEST_1 = baseModel.Entity.TBL_TEST_1.Find(id);
            baseModel.Entity.TBL_TEST_1.Remove(tBL_TEST_1);
            baseModel.Entity.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                baseModel.Entity.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
