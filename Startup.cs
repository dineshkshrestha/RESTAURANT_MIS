using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RESTAURANT_MIS.Startup))]
namespace RESTAURANT_MIS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
