using KronoBattleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Builder
{
    // https://www.journaldev.com/1425/builder-design-pattern-in-java
    // https://refactoring.guru/design-patterns/builder/csharp/example

    public interface IShipBuilder
    {
        void BuildBase();
        void BuildCoordinates();
        void BuildSize();
        void Reset();
    }
    public class ShipBuilder : IShipBuilder
    {
        private Ship _ship = new Ship();
        private Battle Battle;
        private User Owner;
        private int x, endx, y, endy;
        private bool isHorizontal;
        public ShipBuilder(User ownerr, Battle battlee, int xx, int endxx, int yy, int endyy, bool isHorizontall)
        {
            this.Owner = ownerr;
            this.Battle = battlee;
            this.x = xx;
            this.endx = endxx;
            this.y = yy;
            this.endy = endyy;
            this.isHorizontal = isHorizontall;
            this.Reset();
        }
        public void Reset()
        {
            this._ship = new Ship();
        }
        public void BuildBase()
        {
            _ship.Battle = Battle;
            _ship.BattleId = Battle.BattleId;
            _ship.Player = Owner;
            _ship.PlayerName = Owner.UserName;
        }
        public void BuildCoordinates()
        {
            while (x <= endx && y <= endy)
            {
                _ship.Coordinates.Add(new ShipCoordinates(x, y, _ship));
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

        public void BuildSize()
        {
            _ship.Size = _ship.Coordinates.Count;
        }

        public Ship GetShip()
        {
            Ship result = this._ship;

            this.Reset();

            return result;
        }
    }



}