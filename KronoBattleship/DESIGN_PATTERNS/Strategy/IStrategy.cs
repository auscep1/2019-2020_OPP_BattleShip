using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Strategy
{
    public abstract class IStrategy
    {
        public Algorithm algorithm1;

        Algorithm algorithm;

        public void move()
        {

        }

        public void setAlgorithm(Algorithm newAlgorithm)
        {

        }

    }
}