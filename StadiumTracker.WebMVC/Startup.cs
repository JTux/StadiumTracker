using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StadiumTracker.WebMVC.Startup))]
namespace StadiumTracker.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
