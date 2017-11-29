using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GenElect.Startup))]
namespace GenElect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
