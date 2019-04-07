using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InstaRoomWeb.Startup))]
namespace InstaRoomWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
