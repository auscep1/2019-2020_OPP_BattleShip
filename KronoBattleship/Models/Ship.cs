using KronoBattleship.DESIGN_PATTERNS.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    public class Ship : Unit
    {
        public Ship()
        {
            Coordinates = new List<ShipCoordinates>();
        }
        public Ship(User owner, Battle battle)
        {
            PlayerName = owner.UserName;
            Player = owner;
            BattleId = battle.BattleId;
            Battle = battle;
            Coordinates = new List<ShipCoordinates>();
        }
        public int ShipId { get; set; }
        public int Size { get; set; }
        public List<ShipCoordinates> Coordinates { get; set; }
        public string PlayerName { get; set; }
        public virtual User Player { get; set; }
        public int BattleId { get; set; }
        public virtual Battle Battle { get; set; }

        public override string ProductName { get { return "It's Ship"; } }
        public override Unit Product { get { return new Ship(); } }
    }
}