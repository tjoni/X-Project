using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(X_Project.Startup))]
namespace X_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
