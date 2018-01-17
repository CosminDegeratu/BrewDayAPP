using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrewDayAPP.Startup))]
namespace BrewDayAPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
