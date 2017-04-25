using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskApplication.Data;
using TaskApplication.Models;

namespace TaskApplication.Controllers
{
    public class TaskModelsController : Controller
    {
        private MainContext db = new MainContext();

        private  SqliteCalss _db = new SqliteCalss();

        // GET: TaskModels
        public ActionResult Index()
        {            
            var Tasks = _db.LoadData();

            return View(Tasks);
        }

        // GET: TaskModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = _db.findrow(id);//db.Tasks.Find(id));
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            return View(taskModel);
        }

        // GET: TaskModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "oldId,name,desc")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                _db.fillTable(taskModel.name, taskModel.desc);
                return RedirectToAction("Index");
            }

            return View(taskModel);
        }

        // GET: TaskModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = _db.findrow(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            return View(taskModel);
        }

        // POST: TaskModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "oldId,name,desc")] TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                _db.editTable(taskModel.name, taskModel.desc,taskModel.oldId);
                return RedirectToAction("Index");
            }
            return View(taskModel);
        }

        // GET: TaskModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskModel taskModel = _db.findrow(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            return View(taskModel);
        }

        // POST: TaskModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskModel taskModel = _db.findrow(id);//db.Tasks.Find(id);
            if (taskModel == null)
            {
                return HttpNotFound();
            }
            _db.deleterowTable(taskModel.oldId);
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
