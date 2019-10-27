using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.DESIGN_PATTERNS.Prototype_pattern;
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
        public Ship CloneShalow() => (Ship)this.Clone();
        public object ShalowClone()
        {
            return (Ship)this.MemberwiseClone();
        }
        public Ship DeepClone()
        {
            Ship cloned = (Ship)this.Clone();
            cloned.Battle = this.Battle.Copy();
            cloned.BattleId = this.BattleId;
            cloned.Coordinates = this.Coordinates.ToList<ShipCoordinates>();
            cloned.ShipId = this.ShipId;
            cloned.Player = this.Player.Copy();
            cloned.PlayerName = (string)this.PlayerName.Clone();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\nerlai\Desktop\out.txt"))
            {
                file.WriteLine("Origin hash code: {0}", this.GetHashCode());
                file.WriteLine("Cloned hash code: {0}", cloned.GetHashCode());
            }
            return cloned;
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