using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EngeesCollege.Startup))]
namespace EngeesCollege
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
