using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.AbstractFactory
{
    public abstract class AbstractFactory
    {
        public abstract Unit createPlane();

        public abstract Unit createShip();

    }
}