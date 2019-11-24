using KronoBattleship.DESIGN_PATTERNS.Decorator;
using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Interpreter
{
    public abstract class Expression 
    {
        public abstract List<PlaneCoordinates> oneSizerPlane();
        public abstract List<PlaneCoordinates> twoSizerPlane(int x, int y, Unit obj);
        public abstract List<PlaneCoordinates> threeSizerPlane(int x, int y, Unit obj);

        public List<PlaneCoordinates> Interprete(int type, int x, int y, Unit obj)
        {
            System.Diagnostics.Debug.WriteLine("Expression : Interprete type:" + type);
            switch (type)
            {
                case 1:
                    return oneSizerPlane();
                case 2:
                    return twoSizerPlane(x, y, obj);
                case 3:
                    return threeSizerPlane(x, y, obj);
                default:
                    return null;
            }
        }

    }

    public class PlaneCoordinatesInterpreter : Expression
    {
        public override List<PlaneCoordinates> oneSizerPlane()
        {
            System.Diagnostics.Debug.WriteLine("PlaneCoordinatesInterpreter : creating ONE size coordinates");
            var coordinates = new List<PlaneCoordinates>();

            System.Diagnostics.Debug.WriteLine("PlaneFactory : Factory. One sizer Unit created.");

            return coordinates;
        }
        public override List<PlaneCoordinates> twoSizerPlane(int x, int y, Unit obj)
        {
            System.Diagnostics.Debug.WriteLine("PlaneCoordinatesInterpreter : creating TWO size coordinates");
            var coordinates = new List<PlaneCoordinates>();

            SizeBase sizeBase = new SizeBase();
            var sizeDecorator = new SizeDecorator(sizeBase, x);
            System.Diagnostics.Debug.WriteLine("{0} {1}", "PlaneFactory : Factory/Decorator. size added.", sizeDecorator.GetName());
            coordinates.Add(new PlaneCoordinates(sizeDecorator.GetResizer(), y, obj as Plane));
            System.Diagnostics.Debug.WriteLine("PlaneFactory : Factory. Two sizer Unit created.");

            return coordinates;
        }

        public override List<PlaneCoordinates> threeSizerPlane(int x, int y, Unit obj)
        {
            System.Diagnostics.Debug.WriteLine("PlaneCoordinatesInterpreter : creating THREE size coordinates");
            var coordinates = new List<PlaneCoordinates>();

            SizeBase sizeBase = new SizeBase();
            var sizeDecorator = new SizeDecorator(sizeBase, x);
            System.Diagnostics.Debug.WriteLine("{0} {1}", "PlaneFactory : Factory/Decorator. size added.", sizeDecorator.GetName());
            coordinates.Add(new PlaneCoordinates(sizeDecorator.GetResizer(), y, obj as Plane));
            sizeDecorator = new SizeDecorator(sizeBase, sizeDecorator.GetResizer());
            System.Diagnostics.Debug.WriteLine("{0} {1}", "PlaneFactory : Factory/Decorator. size added.", sizeDecorator.GetName());
            coordinates.Add(new PlaneCoordinates(sizeDecorator.GetResizer(), y, obj as Plane));
            System.Diagnostics.Debug.WriteLine("PlaneFactory : Factory. Three sizer Unit created.");

            return coordinates;
        }
    }
}