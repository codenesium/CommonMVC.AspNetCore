using Autofac.Extras.NLog;
using System.Data.Entity;

namespace Codenesium.Foundation.CommonMVC
{
    /// <summary>
    /// All controllers that need transaction support but not authentication should inherit from
    /// this class
    /// </summary>
    public abstract class AbstractPublicApiController : AbstractEntityFrameworkApiController
    {
        public AbstractPublicApiController(
            ILogger logger,
            DbContext context) : base(logger, context)
        {
            this.Context.Configuration.LazyLoadingEnabled = false;
        }
    }
}