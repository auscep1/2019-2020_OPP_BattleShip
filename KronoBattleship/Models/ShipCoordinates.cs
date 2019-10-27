using KronoBattleship.DESIGN_PATTERNS.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    public class ShipCoordinates
    {
        public ShipCoordinates() { }
        public ShipCoordinates(int xx, int yy, Ship ship)
        {
            Ship = ship;
            ShipId = ship.ShipId;
            x = xx;
            y = yy;
        }
        public ShipCoordinates(int xx, int yy, Plane plane)
        {
            Plane = plane;
            ShipId = plane.PlaneId;
            x = xx;
            y = yy;
        }
        public int ShipCoordinatesId { get; set; }
        public int ShipId { get; set; }
        public virtual Ship Ship { get; set; }
        public virtual Plane Plane { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public bool Alive { get; set; } = true;
    }
}