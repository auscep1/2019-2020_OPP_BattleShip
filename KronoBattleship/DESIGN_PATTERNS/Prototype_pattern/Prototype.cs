using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Prototype_pattern
{
    public interface IPrototype
    {
        object Clone();
    }
}