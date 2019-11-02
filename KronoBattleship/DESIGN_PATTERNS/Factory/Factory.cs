using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using KronoBattleship.DESIGN_PATTERNS.Decorator;
using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.Factory
{
    //https://dev.to/gary_woodfine/how-to-use-factory-method-design-pattern-in-c-3ia3
    //https://www.c-sharpcorner.com/article/factory-method-design-pattern-in-c-sharp/
    public abstract class Factory
    {
        public abstract Unit GetUnit(int switchToWhichUnit, User ownerr, Battle battle, int xx, int endxx, int yy, int endyy, bool isHorizontall, int type);
    }
    public class PlaneFactory : Factory
    {
        /// <summary>
        /// Get Unit object
        /// </summary>
        /// <param name="switchToWhichUnit"></param>
        /// <param name="ownerr"></param>
        /// <param name="battle"></param>
        /// <param name="x"></param>
        /// <param name="endx"></param>
        /// <param name="y"></param>
        /// <param name="endy"></param>
        /// <param name="isHorizontal"></param>
        /// <returns></returns>
        public override Unit GetUnit(int switchToWhichUnit, User ownerr, Battle battle, int x, int endx, int y, int endy, bool isHorizontal, int type)
        {
            switch (switchToWhichUnit)
            {
                case 1:
                    System.Diagnostics.Debug.WriteLine("PlaneFactory : Factory: Get Unit.");
                    return CreatePlane(ownerr, battle, x, endx, y, endy, isHorizontal, type);
                default:
                    return null;
            }
        }
        /// <summary>
        /// Create Plane object
        /// </summary>
        /// <param name="ownerr"></param>
        /// <param name="battle"></param>
        /// <param name="x"></param>
        /// <param name="endx"></param>
        /// <param name="y"></param>
        /// <param name="endy"></param>
        /// <param name="isHorizontal"></param>
        /// <returns></returns>
        private Unit CreatePlane(User ownerr, Battle battle, int x, int endx, int y, int endy, bool isHorizontal, int type)
        {
            Unit obj = new Plane(ownerr, battle);
            /**decorator implementation**/
            obj.Coordinates.Add(new PlaneCoordinates(x, y, obj as Plane));
            SizeDecorator sizeDecorator;
            SizeBase sizeBase = new SizeBase();
            switch (type)
            {
                case 1: //one sizer Plane
                    System.Diagnostics.Debug.WriteLine("PlaneFactory : Factory. One sizer Unit created.");
                    break;
                case 2: //two sizer Plane
                    sizeDecorator = new SizeDecorator(sizeBase, x);
                    System.Diagnostics.Debug.WriteLine("{0} {1}", "PlaneFactory : Factory/Decorator. size added.", sizeDecorator.GetName());
                    obj.Coordinates.Add(new PlaneCoordinates(sizeDecorator.GetResizer(), y, obj as Plane));
                    System.Diagnostics.Debug.WriteLine("PlaneFactory : Factory. Two sizer Unit created.");
                    break;
                case 3: //three sizer Plane
                    sizeDecorator = new SizeDecorator(sizeBase, x);
                    System.Diagnostics.Debug.WriteLine("{0} {1}", "PlaneFactory : Factory/Decorator. size added.", sizeDecorator.GetName());
                    obj.Coordinates.Add(new PlaneCoordinates(sizeDecorator.GetResizer(), y, obj as Plane));
                    sizeDecorator = new SizeDecorator(sizeBase, sizeDecorator.GetResizer());
                    System.Diagnostics.Debug.WriteLine("{0} {1}", "PlaneFactory : Factory/Decorator. size added.", sizeDecorator.GetName());
                    obj.Coordinates.Add(new PlaneCoordinates(sizeDecorator.GetResizer(), y, obj as Plane));
                    System.Diagnostics.Debug.WriteLine("PlaneFactory : Factory. Three sizer Unit created.");
                    break;
            }
            //while (x <= endx && y <= endy)
            //{
            //    obj.Coordinates.Add(new PlaneCoordinates(x, y, obj as Plane));
            //    if (isHorizontal)
            //    {
            //        x++;
            //    }
            //    else
            //    {
            //        y++;
            //    }
            //}
            obj.Size = obj.Coordinates.Count;
            System.Diagnostics.Debug.WriteLine(obj.ProductName);
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