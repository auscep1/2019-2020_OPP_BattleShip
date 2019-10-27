using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.Factory
{
    //https://dev.to/gary_woodfine/how-to-use-factory-method-design-pattern-in-c-3ia3
    //https://www.c-sharpcorner.com/article/factory-method-design-pattern-in-c-sharp/
    public abstract class Factory
    {
        public abstract Unit GetUnit();
        public abstract void Base();
        public abstract void Coordinates();
        public abstract void Size();
        public abstract void Reset();
    }
    public class PlaneFactory : Factory
    {
        private Unit _plane = new Plane();
        private Battle Battle;
        private User Owner;
        private int x, endx, y, endy;
        private bool isHorizontal;
        public PlaneFactory(User ownerr, Battle battle, int xx, int endxx, int yy, int endyy, bool isHorizontall)
        {
            this.Owner = ownerr;
            this.Battle = battle;
            this.x = xx;
            this.endx = endxx;
            this.y = yy;
            this.endy = endyy;
            this.isHorizontal = isHorizontall;
            this.Reset();
        }

        public override void Base()
        {
            _plane.BattleId = Battle.BattleId;
            _plane.PlayerName = Owner.UserName;

            _plane.Battle = Battle;
            _plane.Player = Owner;
        }

        public override void Coordinates()
        {
            while (x <= endx && y <= endy)
            {
                _plane.Coordinates.Add(new ShipCoordinates(x, y, _plane as Plane));
                if (isHorizontal)
                {
                    x++;
                }
                else
                {
                    y++;
                }
            }
        }

        public override Unit GetUnit()
        {
            return new Plane(Owner, Battle);
        }

        public override void Reset()
        {
            this._plane = new Plane();
        }

        public override void Size()
        {
            _plane.Size = _plane.Coordinates.Count;
        }
        /**public static Unit GetUnit(int id)
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
        }**/
    }
}