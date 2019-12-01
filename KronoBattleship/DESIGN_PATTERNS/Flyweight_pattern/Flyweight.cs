using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.Datalayer;
using KronoBattleship.DESIGN_PATTERNS.State;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.Flyweight_pattern
{
    class FlyweightFactory
    {
        private Dictionary<char, State.State> flyweights = new Dictionary<char, State.State>();
        public State.State GetFlyweight(char key)
        {
            State.State flyweight;
            if (flyweights.ContainsKey(key))
            {
                flyweight = flyweights[key];
            }
            else
            {
                switch (key)
                {
                    case 'C':
                        flyweight = new ConcreteStateConnected();
                        break;
                    case 'W':
                        flyweight = new Waiting();
                        break;
                    case 'P':
                        flyweight = new Playing();
                        break;
                    default:
                        return null;
                }
                flyweights.Add(key, flyweight);
            }
            return flyweight;
        }
    }
}