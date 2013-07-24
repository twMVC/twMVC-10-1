using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiProjects.Web.MVC.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.Repository = MvcApplication.RepositoryType;
        }

    }
}
