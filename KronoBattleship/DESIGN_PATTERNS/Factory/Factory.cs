using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.Factory
{
    //https://dev.to/gary_woodfine/how-to-use-factory-method-design-pattern-in-c-3ia3
    //https://www.c-sharpcorner.com/article/factory-method-design-pattern-in-c-sharp/
    public abstract class Factory
    {
        public abstract Unit GetUnit(int switchToWhichUnit, User ownerr, Battle battle, int xx, int endxx, int yy, int endyy, bool isHorizontall);
        // public abstract void Base();
        //public abstract void Coordinates();
        //public abstract void Size();
        // public abstract void Reset();
    }
    public class PlaneFactory : Factory
    {
        public override Unit GetUnit(int switchToWhichUnit, User ownerr, Battle battle, int x, int endx, int y, int endy, bool isHorizontal)
        {
            switch (switchToWhichUnit)
            {
                case 1:
                    return CreatePlane(ownerr, battle, x, endx, y, endy, isHorizontal);
                default:
                     return null;
            }
        }
        private Unit CreatePlane(User ownerr, Battle battle, int x, int endx, int y, int endy, bool isHorizontal)
        {
            Unit obj = new Plane(ownerr, battle);
            while (x <= endx && y <= endy)
            {
                obj.Coordinates.Add(new PlaneCoordinates(x, y, obj as Plane));
                if (isHorizontal)
                {
                    x++;
                }
                else
                {
                    y++;
                }
            }
            obj.Size = obj.Coordinates.Count;
            System.Diagnostics.Debug.WriteLine("PlaneFactory.GetUnit: Plane created: "+obj.ProductName);
            return obj;
        }


        /**public static Unit GetUnit(int id)
        {
            switch (id)
            {
                case 1:
                    return new Ship();
                case 2:
                    return new Plane();
                default:
                    return new Ship();
            }
        }**/
    }
}