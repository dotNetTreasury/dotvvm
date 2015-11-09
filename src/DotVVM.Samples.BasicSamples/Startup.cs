using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.Web.Hosting;

[assembly: OwinStartup(typeof(DotVVM.Samples.BasicSamples.Startup))]

namespace DotVVM.Samples.BasicSamples
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;

            // use DotVVM
            DotvvmConfiguration dotvvmConfiguration = app.UseDotVVM(applicationPhysicalPath);
            dotvvmConfiguration.RouteTable.RegisterRoutingStrategy(new ViewsFolderDothmlFileBasedRouteStrategy(dotvvmConfiguration));

            // use static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)
            });
        }
    }
}