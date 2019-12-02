using System.Collections.Generic;
using static KronoBattleship.DESIGN_PATTERNS.Command.Command;

namespace KronoBattleship.DESIGN_PATTERNS.Visitor
{
    interface IVisitor
    {
        List<int> visit(GetExtraSmallShipLocation getExtraSmallShipLocation, bool isHorizontal, int iteration);
        List<int> visit(GetSmallShipLocation getSmallShipLocation, bool isHorizontal, int iteration);
        List<int> visit(GetNormalShipLocation getNormalShipLocation, bool isHorizontal, int iteration);
        List<int> visit(GetLargeShipLocation getLargeShipLocation, bool isHorizontal, int iteration);
        List<int> visit(GetExtraLargeShipLocation getExtraLargeShipLocation, bool isHorizontal, int iteration);
    }
}
