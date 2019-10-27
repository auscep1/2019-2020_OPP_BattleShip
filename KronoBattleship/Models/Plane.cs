using KronoBattleship.DESIGN_PATTERNS.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    public class Plane : Unit
    {
        public Plane()
        {
            Coordinates = new List<ShipCoordinates>();
        }
        public Plane(User owner, Battle battle)
        {
            PlayerName = owner.UserName;
            Player = owner;
            BattleId = battle.BattleId;
            Battle = battle;
            Coordinates = new List<ShipCoordinates>();
        }
        //public int PlaneId { get; set; }

        public int PlaneId { get; set; }
       // public override int UnitId { get { return PlaneId; } }
        public override int Size { get; set; }
        public  override List<ShipCoordinates> Coordinates { get; set; }
        public override string PlayerName { get; set; }
        public override int BattleId { get; set; }

        public new User Player { get; set; }
        public new Battle Battle { get; set; }

        /*temorrary*/
        public override string ProductName { get { return "It's Plane"; } }
        public override Unit UnitObj { get { return new Plane(); } }

    }
}