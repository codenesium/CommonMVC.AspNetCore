using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Codenesium.Foundation.CommonMVC
{
    /// <summary>
    /// This attribute can be added to any controller method that needs transaction support.
    /// </summary>
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            AbstractEntityFrameworkApiController controller = (AbstractEntityFrameworkApiController)actionContext.ControllerContext.Controller;
            controller.Context.Database.BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            AbstractEntityFrameworkApiController controller = (AbstractEntityFrameworkApiController)actionExecutedContext.ActionContext.ControllerContext.Controller;

            if (actionExecutedContext.Exception == null)
            {
                try
                {
                    if (controller.Context.Database.CurrentTransaction != null)
                    {
                        controller.Context.Database.CurrentTransaction.Commit();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    controller.Context.Dispose();
                }
            }
            else
            {
                try
                {
                    if (controller.Context.Database.CurrentTransaction != null)
                    {
                        controller.Context.Database.CurrentTransaction.Rollback();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    controller.Context.Dispose();
                }
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}