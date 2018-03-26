using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Nine.Core.IoC;
using System.Web.Http;
using System.Web.Mvc;

namespace Nine.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public IContainer Container { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Register IoC container
            var reg = new AppRegistration();
            Container = reg.Register();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
