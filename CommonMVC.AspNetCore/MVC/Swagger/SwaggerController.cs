using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Codenesium.Foundation.CommonMVC.MVC
{
    /// <summary>
    /// The purpose of this contrller is to allow a redirect in
    /// a WepAPI project to the swagger page.
    /// http://stackoverflow.com/questions/33891721/asp-net-webapi-default-landing-page
    /// </summary>
    public class SwaggerController : Controller
    {
        // GET: Browser
        public ActionResult Index()
        {
            return Redirect("swagger");
        }
    }
}