using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(makeup1.Startup))]
namespace makeup1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
