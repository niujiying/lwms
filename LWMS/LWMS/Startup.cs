using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LWMS.Startup))]
namespace LWMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
