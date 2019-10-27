using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.DESIGN_PATTERNS.Prototype_pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    public class Plane : Unit, IPrototype
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
        public Plane CloneShalow() => (Plane)this.Clone();
        public object ShalowClone()
        {
            return (Plane)this.MemberwiseClone();
        }
        public Plane DeepClone()
        {
            Plane cloned = (Plane)this.Clone();
            cloned.Battle = this.Battle.Copy();
            cloned.BattleId = this.BattleId;
            cloned.Coordinates = this.Coordinates.ToList<ShipCoordinates>();
            cloned.PlaneId = this.PlaneId;
            cloned.Player = this.Player.Copy();
            cloned.PlayerName = (string)this.PlayerName.Clone();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\nerlai\Desktop\out.txt"))
            {
                file.WriteLine("Origin hash code: {0}", this.GetHashCode());
                file.WriteLine("Cloned hash code: {0}", cloned.GetHashCode());
            }
            return cloned;
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