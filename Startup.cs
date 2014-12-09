using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TMID.Startup))]
namespace TMID
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
