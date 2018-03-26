using Autofac;
using Nine.Core.Helpers;
using Nine.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nine.Core.IoC.Installer
{
    public class HelperInstallerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JsonHelper>().As<IJsonHelper>().SingleInstance();
        }
    }
}
