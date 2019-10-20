using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.AbstractFactory
{
    public class SingleDeckerFactory : AbstractFactory
	{
        public SingleDeckerFactory()
        {
        }

        public override Unit createPlane()
        {
            throw new NotImplementedException();
        }

        public override Unit createShip()
        {
            throw new NotImplementedException();
        }
    }
	
}
