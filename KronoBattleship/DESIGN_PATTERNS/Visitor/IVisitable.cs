using System.Collections.Generic;

namespace KronoBattleship.DESIGN_PATTERNS.Visitor
{
    interface IVisitable
    {
        List<int> accept(IVisitor visit, bool isHorizontal, int iteration);
    }
}
