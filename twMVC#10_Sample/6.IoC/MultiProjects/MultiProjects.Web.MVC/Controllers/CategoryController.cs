using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiProjects.Domain;
using MultiProjects.Repository.Interface;

namespace MultiProjects.Web.MVC.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository r)
        {
            this._repository = r;
        }


        public ActionResult Index()
        {
            var categories = this._repository.GetCategories();
            return View(categories);
        }

        public ActionResult Details(int id)
        {
            var item = this._repository.GetOne(id);
            return View(item);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category instance)
        {
            if (ModelState.IsValid)
            {
                this._repository.Create(instance);
                return RedirectToAction("Index");
            }
            return View(instance);
        }

        public ActionResult Edit(int id = 0)
        {
            Category instance = this._repository.GetOne(id);
            if (instance == null)
            {
                return HttpNotFound();
            }
            return View(instance);
        }

        [HttpPost]
        public ActionResult Edit(Category instance)
        {
            if (ModelState.IsValid)
            {
                this._repository.Update(instance);
                return RedirectToAction("Index");
            }
            return View(instance);
        }

        public ActionResult Delete(int id = 0)
        {
            Category instance = this._repository.GetOne(id);
            if (instance == null)
            {
                return HttpNotFound();
            }
            return View(instance);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            this._repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
