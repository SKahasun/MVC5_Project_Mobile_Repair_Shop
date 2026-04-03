using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MasterDetails_MobileFixCenter.Startup))]
namespace MasterDetails_MobileFixCenter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
