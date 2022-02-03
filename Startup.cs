using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_1263087.Startup))]
namespace _1263087
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
