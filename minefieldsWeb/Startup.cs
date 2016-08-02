using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(minefieldsWeb.Startup))]
namespace minefieldsWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
