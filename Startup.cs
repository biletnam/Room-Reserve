using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RoomReserve.Startup))]
namespace RoomReserve
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
