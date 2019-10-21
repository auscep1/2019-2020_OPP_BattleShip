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
        public new List<ShipCoordinates> Coordinates { get; set; } //added "new"
        public new string PlayerName { get; set; }
        public new virtual User Player { get; set; } //auscep added "new"
        public new int BattleId { get; set; }
        public new virtual Battle Battle { get; set; }
    }
}