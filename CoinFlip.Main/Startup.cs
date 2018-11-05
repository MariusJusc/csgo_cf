using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoinFlip.Main.Startup))]
namespace CoinFlip.Main
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
