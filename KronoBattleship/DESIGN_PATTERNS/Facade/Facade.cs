using KronoBattleship.DESIGN_PATTERNS.Builder;
using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Facade
{
    public class Facade
    {
        private Battle Battle;
        private User Owner;
        private int x, endx, y, endy;
        private bool isHorizontal;

        public Facade(User ownerr, Battle battlee, int xx, int endxx, int yy, int endyy, bool isHorizontall)
        {
            Owner = ownerr;
            Battle = battlee;
            x = xx;
            endx = endxx;
            y = yy;
            endy = endyy;
            isHorizontal = isHorizontall;
        }
        public Ship GetShip()
        {
            //var builder = new ShipBuilder(Owner, Battle, x, endx, y, endy, isHorizontal);
            IShipBuilder shipabstractbuilder = new ShipBuilder(Owner, Battle, x, endx, y, endy, isHorizontal);
            AbstractShipBuilder builder = new AbstractShipBuilder(shipabstractbuilder);
            builder.BuildBase();
            builder.BuildCoordinates();
            builder.BuildSize();

            return builder.GetShip();
        }

        /// <summary>
        /// Returns new Plane
        /// </summary>
        /// <returns>Plane</returns>
        public Unit GetPlane()
        {
            PlaneFactory f = new PlaneFactory();
            var plane = f.GetUnit(1,Owner, Battle, x, endx, y, endy, isHorizontal);
            return plane;
            /**auscep1 uzkomentinta 20191030**/
            //PlaneFactory factory = new PlaneFactory(Owner, Battle, x, endx, y, endy, isHorizontal);
            //factory.Base();
            //factory.Coordinates();
            //factory.Size();
            //return factory.GetUnit();
        }
    }
}