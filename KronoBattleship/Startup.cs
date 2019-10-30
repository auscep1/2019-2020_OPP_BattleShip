using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KronoBattleship.Startup))]
namespace KronoBattleship
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            ////Prototype patikrinimui. NETRINTI!
            //var user = new User();
            //user.UserName = "123";
            //var user1 = new User();
            //user1.UserName = "123";
            //var plane = new Plane(user, new Battle(user, user1));
            //plane.DeepClone();
        }
    }
}
