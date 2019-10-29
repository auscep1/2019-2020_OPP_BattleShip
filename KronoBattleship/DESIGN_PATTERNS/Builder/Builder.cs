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
    //https://www.dofactory.com/net/bridge-design-pattern

    public interface IShipBuilder
    {
        void BuildBase();
        void BuildCoordinates();
        void BuildSize();
        void Reset();
        Ship GetShip();
    }
    public class AbstractShipBuilder : IShipBuilder
    {
        IShipBuilder _someBuilder;
        public AbstractShipBuilder(IShipBuilder someBuilder)
        {
            _someBuilder = someBuilder;
        }
        public void BuildBase()
        {
            _someBuilder.BuildBase();
        }

        public void BuildCoordinates()
        {
            _someBuilder.BuildCoordinates();
        }

        public void BuildSize()
        {
            _someBuilder.BuildSize();
        }

        public void Reset()
        {
            _someBuilder.Reset();
        }
        public Ship GetShip()
        {
            return _someBuilder.GetShip();
        }
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
            System.Diagnostics.Debug.WriteLine("ShipBuilder: Ship base builded");
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
            System.Diagnostics.Debug.WriteLine("ShipBuilder: Ship coordinates builded");
        }

        public void BuildSize()
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