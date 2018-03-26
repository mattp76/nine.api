using Autofac;
using Nine.Core.Interfaces;
using Nine.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nine.Core.IoC.Installer
{
    public class ServiceInstallerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShowService>().As<IShowService>().SingleInstance();
        }
    }
}
