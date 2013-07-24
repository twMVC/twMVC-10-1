using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Repository.Interface;

namespace Sample.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _repository;

        public CategoryController(ICategoryRepository r)
        {
            this._repository = r;
        }

        public ActionResult Index()
        {
            ViewBag.RepositoryName = MvcApplication.RepositoryType;
            var result = this._repository.GetCategories();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            ViewBag.RepositoryName = MvcApplication.RepositoryType;
            var result = this._repository.GetOne(id);
            return View(result);
        }

    }
}
