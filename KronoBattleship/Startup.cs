using KronoBattleship.DESIGN_PATTERNS.Factory;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KronoBattleship.Startup))]
namespace KronoBattleship
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            /**FACTORY IS WORKING**/
            /** 
            string a = Factory.Get(1).ProductName;
             string b = Factory.Get(2).ProductName;

             var aa = Factory.Get(1).Product;
             var bb = Factory.Get(2).Product;
           **/
         /**   Factory factory = null;
            factory = new PlaneFactory(new Models.User(), new Models.Battle(), 1, 2, 1, 1, true);
            Unit unit = factory.GetUnit();
            
            string a = unit.ProductName;
         **/
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
