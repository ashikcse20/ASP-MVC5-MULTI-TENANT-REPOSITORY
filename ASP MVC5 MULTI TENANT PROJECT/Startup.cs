using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspMvc5MultiTenantProject.Startup))]
namespace AspMvc5MultiTenantProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
