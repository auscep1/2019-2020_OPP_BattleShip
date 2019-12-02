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
            if (flyweights.ContainsKey(key))
            {
                return flyweights[key];
            }
            else
            {
                switch (key)
                {
                    case 'W':
                        flyweights.Add(key, new Waiting());
                        return flyweights[key];
                    case 'J':
                        flyweights.Add(key, new Joined());
                        return flyweights[key];
                    case 'R':
                        flyweights.Add(key, new Ready());
                        return flyweights[key];
                    case 'P':
                        flyweights.Add(key, new Playing());
                        return flyweights[key];
                    default:
                        return null;
                }
            }
        }
    }
}