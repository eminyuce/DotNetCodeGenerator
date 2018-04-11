using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotNetCodeGenerator.Startup))]
namespace DotNetCodeGenerator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
