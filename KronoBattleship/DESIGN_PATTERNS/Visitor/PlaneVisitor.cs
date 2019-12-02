using System.Collections.Generic;
using static KronoBattleship.DESIGN_PATTERNS.Command.Command;

namespace KronoBattleship.DESIGN_PATTERNS.Visitor
{
    public class PlaneVisitor : IVisitor
    {
        public List<int> visit(GetExtraSmallShipLocation getExtraSmallShipLocation, bool isHorizontal, int iteration)
        {
            var x = iteration % 10;
            var y = iteration / 10;

            int endx = isHorizontal ? x + 1 : x;
            int endy = isHorizontal ? y : y + 1;

            System.Diagnostics.Debug.WriteLine("Visitor: Location of extra small ship was found with plane visitor");

            return new List<int>() { endx, endy };
        }

        public List<int> visit(GetSmallShipLocation getSmallShipLocation, bool isHorizontal, int iteration)
        {
            var x = iteration % 10;
            var y = iteration / 10;

            int endx = isHorizontal ? x + 1 : x;
            int endy = isHorizontal ? y : y + 1;

            System.Diagnostics.Debug.WriteLine("Visitor: Location of small ship was found with plane visitor");

            return new List<int>() { endx, endy };
        }

        public List<int> visit(GetNormalShipLocation getNormalShipLocation, bool isHorizontal, int iteration)
        {
            var x = iteration % 10;
            var y = iteration / 10;

            int endx = isHorizontal ? x + 2 : x;
            int endy = isHorizontal ? y : y + 2;

            System.Diagnostics.Debug.WriteLine("Visitor: Location of normal ship was found with plane visitor");

            return new List<int>() { endx, endy };
        }

        public List<int> visit(GetLargeShipLocation getLargeShipLocation, bool isHorizontal, int iteration)
        {
            var x = iteration % 10;
            var y = iteration / 10;

            int endx = isHorizontal ? x + 2 : x;
            int endy = isHorizontal ? y : y + 2;

            System.Diagnostics.Debug.WriteLine("Visitor: Location of large ship was found with plane visitor");

            return new List<int>() { endx, endy };
        }

        public List<int> visit(GetExtraLargeShipLocation getExtraLargeShipLocation, bool isHorizontal, int iteration)
        {
            var x = iteration % 10;
            var y = iteration / 10;

            int endx = isHorizontal ? x + 4 : x;
            int endy = isHorizontal ? y : y + 4;

            System.Diagnostics.Debug.WriteLine("Visitor: Location of extra large ship was found with plane visitor");

            return new List<int>() { endx, endy };
        }
    }
}