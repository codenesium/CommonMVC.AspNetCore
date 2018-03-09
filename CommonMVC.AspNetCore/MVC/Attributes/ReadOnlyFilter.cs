using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Codenesium.Foundation.CommonMVC
{
    /// <summary>
    /// This attribute can be added to disable entity framework change tracking
    /// </summary>
    public class ReadOnlyFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            AbstractEntityFrameworkApiController controller = (AbstractEntityFrameworkApiController)actionContext.ControllerContext.Controller;
            controller.Context.Configuration.AutoDetectChangesEnabled = false; //disable change tracking in context
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}