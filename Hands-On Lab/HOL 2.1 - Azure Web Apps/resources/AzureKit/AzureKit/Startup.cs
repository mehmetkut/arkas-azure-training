using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureKit.Startup))]
namespace AzureKit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
