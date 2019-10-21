using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.Factory
{
    public class Factory
    {
        public static Unit Get(int id)
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
        }
    }
}