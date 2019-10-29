using KronoBattleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Builder
{
    // https://www.journaldev.com/1425/builder-design-pattern-in-java
    // https://refactoring.guru/design-patterns/builder/csharp/example


    //https://www.dotnettricks.com/learn/designpatterns/bridge-design-pattern-dotnet
    //https://www.geeksforgeeks.org/bridge-design-pattern/

    public interface IShipBuilder
    {
        void BuildBase();
        void BuildCoordinates();
        void BuildSize();
        void Reset();
    }
    public abstract class AbstractShipBuilder : IShipBuilder
    {
        Ship _ship;
        public virtual void BuildBase()
        {
            _ship = new Ship();
        }

        public virtual void BuildCoordinates()
        {
            _ship.Coordinates = new List<ShipCoordinates>();
        }

        public virtual void BuildSize()
        {
            _ship.Size = _ship.Coordinates.Count;
        }

        public virtual void Reset()
        {
            _ship = new Ship();
        }
    }
    public class ShipBuilder : AbstractShipBuilder
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
        //public void Reset()
        //{
        //    this._ship = new Ship();
        //}
        public override void BuildBase()
        {
            _ship.Battle = Battle;
            _ship.BattleId = Battle.BattleId;
            _ship.Player = Owner;
            _ship.PlayerName = Owner.UserName;
            System.Diagnostics.Debug.WriteLine("ShipBuilder: Ship base builded");
        }
        public override void BuildCoordinates()
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
            System.Diagnostics.Debug.WriteLine("ShipBuilder: Ship coordinates builded");
        }

        public override void BuildSize()
        {
            _ship.Size = _ship.Coordinates.Count;
            System.Diagnostics.Debug.WriteLine("ShipBuilder: Ship size builded");
        }

        public Ship GetShip()
        {
            Ship result = this._ship;

            this.Reset();
            System.Diagnostics.Debug.WriteLine("ShipBuilder: Returning builded ship");
            return result;
        }
    }





}