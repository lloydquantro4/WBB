using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBank.Startup))]
namespace WebBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
