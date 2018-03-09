using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Codenesium.Foundation.CommonMVC
{
    /// <summary>
    /// The purpose of this filter is to throw an error when a client passes a null model to a controller
    /// from http://stackoverflow.com/questions/14517151/how-to-ensure-asp-net-web-api-controllers-parameter-is-not-null
    /// </summary>
    public class ModelValidateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.Any(v => v.Value == null))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}