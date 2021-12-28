using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Emlak_Sitesi.Startup))]
namespace MVC_Emlak_Sitesi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
