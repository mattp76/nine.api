using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Reflection;

namespace Nine.Core.IoC
{
    public class AppRegistration
    {
        public IContainer Register()
        {
            var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(domainAssemblies);
            builder.RegisterApiControllers(domainAssemblies);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyModules(domainAssemblies);

            IContainer container = builder.Build();

            return container;

        }
    }
}
