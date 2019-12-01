using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.DESIGN_PATTERNS.State;
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
            //Plane deepCopy = plane.DeepClone();
            //Plane shaloowCopy = (Plane)plane.ShallowClone();
            //System.Diagnostics.Debug.WriteLine("ShalowClone()");
            //System.Diagnostics.Debug.WriteLine("Origin hash code: {0}", plane.GetHashCode());
            //System.Diagnostics.Debug.WriteLine("Cloned hash code: {0}", shaloowCopy.GetHashCode());
            //System.Diagnostics.Debug.WriteLine("DeepClone()");
            //System.Diagnostics.Debug.WriteLine("Origin hash code: {0}", plane.GetHashCode());
            //System.Diagnostics.Debug.WriteLine("Cloned hash code: {0}", deepCopy.GetHashCode());

            //Battle b = new Battle();
            //State obj = new Waiting(b, "player");
            //ContextState c = new ContextState(obj,b, "player" );
            //c.RequestPlayer();
            //c.RequestPlayer();
            //c.RequestPlayer();
            //c.RequestPlayer();
        }
    }
}
