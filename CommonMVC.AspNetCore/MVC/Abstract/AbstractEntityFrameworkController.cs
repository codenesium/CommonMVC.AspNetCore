using Autofac.Extras.NLog;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;

namespace Codenesium.Foundation.CommonMVC
{
    /// <summary>
    /// This is the base controller for any controller that needs transaction support.
    /// We use an action filter to start and commit transactions using the Conttext.
    /// </summary>
    public abstract class AbstractEntityFrameworkController : Controller
    {
        public System.Data.Entity.DbContext Context { get; private set; }
        protected ILogger _logger { get; set; }

        public AbstractEntityFrameworkController(
            ILogger logger,
            DbContext context

            )
        {
            this._logger = logger;
            this.Context = context;
            this.Context.Configuration.LazyLoadingEnabled = false;
        }
    }
}