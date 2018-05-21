using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HallManagementSystem.Startup))]
namespace HallManagementSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
