using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.DESIGN_PATTERNS.Flyweight_pattern;
using KronoBattleship.DESIGN_PATTERNS.State;
using KronoBattleship.Models;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(KronoBattleship.Startup))]
namespace KronoBattleship
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            #region Prototype patikrinimui
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
            #endregion
            #region Flyweight atminties naudojimas
            //mem(50);
            //mem(50);
            //mem(100);
            //mem(200);
            //mem(300);
            //mem(400);
            //mem(500);
            #endregion
        }
        public void mem(int n)
        {
            //Flyweight atminties matavimas
            FlyweightFactory f = new FlyweightFactory();
            State[] s = new State[n];
            State[] s1 = new State[n];
            GC.Collect();
            GC.Collect();
            GC.Collect();
            long kbBefore = GC.GetTotalMemory(true);
            for (int i = 0; i < n; i++)
            {
                s[i] = f.GetFlyweight('W');
            }
            long kbAfter1 = GC.GetTotalMemory(false);
            long kbAfter2 = GC.GetTotalMemory(true);
            System.Diagnostics.Debug.WriteLine("Flyweight: memory usage with flyweight: n = {0}, {1}", n, kbAfter2 - kbBefore);
            GC.Collect();
            GC.Collect();
            GC.Collect();
            kbBefore = GC.GetTotalMemory(true);
            for (int i = 0; i < n; i++)
            {
                s1[i] = new Waiting();
            }
            kbAfter1 = GC.GetTotalMemory(false);
            kbAfter2 = GC.GetTotalMemory(true);
            System.Diagnostics.Debug.WriteLine("Flyweight: memory usage without flyweight: n = {0}, {1}",n, kbAfter2 - kbBefore);
        }
    }
}
