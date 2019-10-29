using KronoBattleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KronoBattleship.DESIGN_PATTERNS.Adapter
{
    interface Adapter
    {
        object GetObject();
    }
    public class ShipAdapter : Adapter
    {
        private Battle Battle;
        private User Owner;
        private int x, endx, y, endy;
        private bool isHorizontal;

        public ShipAdapter(User ownerr, Battle battlee, int xx, int endxx, int yy, int endyy, bool isHorizontall)
        {
            Owner = ownerr;
            Battle = battlee;
            x = xx;
            endx = endxx;
            y = yy;
            endy = endyy;
            isHorizontal = isHorizontall;
        }
        public object GetObject()
        {
            Facade.Facade facade = new Facade.Facade(Owner, Battle, x, endx, y, endy, isHorizontal);
            System.Diagnostics.Debug.WriteLine("Facade: Object was created");
            System.Diagnostics.Debug.WriteLine("ShipAdapter: Returning created ship");
            return facade.GetShip();
        }
    }
    public class PlaneAdapter : Adapter
    {
        private Battle Battle;
        private User Owner;
        private int x, endx, y, endy;
        private bool isHorizontal;

        public PlaneAdapter(User ownerr, Battle battlee, int xx, int endxx, int yy, int endyy, bool isHorizontall)
        {
            Owner = ownerr;
            Battle = battlee;
            x = xx;
            endx = endxx;
            y = yy;
            endy = endyy;
            isHorizontal = isHorizontall;
        }
        public object GetObject()
        {
            Factory.PlaneFactory facade = new Factory.PlaneFactory(Owner, Battle, x, endx, y, endy, isHorizontal);
            System.Diagnostics.Debug.WriteLine("PlaneAdapter: Returning created plane");
            return facade.GetUnit();
        }
    }
}
