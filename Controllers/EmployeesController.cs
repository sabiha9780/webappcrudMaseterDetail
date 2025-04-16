using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppCurdMD.Models;
using WebAppCurdMD.ViewModel;

namespace WebAppCurdMD.Controllers
{
    public class EmployeesController : Controller
    {
        private ExamDatabaseEntities db = new ExamDatabaseEntities();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View(new EmployeeVM());
        }



        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Designation,JoinDate,Gender,Salary,IsActive, Experiences, operation")] EmployeeVM employee)
        {

            if (employee.Operation == "Add")
            {
                if (employee.Experiences == null)
                {
                    employee.Experiences = new List<Experience>();
                }
                employee.Experiences.Add(new Experience());
                return View(employee);
            }

            if (employee.Operation !=null&& employee.Operation.StartsWith("Delete"))
            {
                int.TryParse(employee.Operation.Replace("Delete-", ""), out int index);
                employee.Experiences.RemoveAt(index);
                return View(employee);
            }

            if (ModelState.IsValid)
            {var emp =employee.ToEmployee();
                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var data =new EmployeeVM(employee);
            return View(data);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Designation,JoinDate,Gender,Salary,IsActive, Experiences, operation")] EmployeeVM employee)
        {

            if (employee.Operation == "Add")
            {
                if (employee.Experiences == null)
                {
                    employee.Experiences = new List<Experience>();
                }
                employee.Experiences.Add(new Experience());
                return View(employee);
            }

            if (employee.Operation != null && employee.Operation.StartsWith("Delete"))
            {
                int.TryParse(employee.Operation.Replace("Delete-", ""), out int index);
                employee.Experiences.RemoveAt(index);
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                Employee del = db.Employees.Find(employee.Id);
                db.Experiences.RemoveRange(del.Experiences);
                db.Employees.Remove(del);
                db.SaveChanges();



                var emp =employee.ToEmployee();
                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            db.Experiences.RemoveRange(employee.Experiences);
            db.Employees.Remove(employee);
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
