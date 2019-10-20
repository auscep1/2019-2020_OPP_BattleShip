using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.AbstractFactory
{
    public class TwoDeckerFactory : AbstractFactory
	{
        public TwoDeckerFactory()
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
