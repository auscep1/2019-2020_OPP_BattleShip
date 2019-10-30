using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.DESIGN_PATTERNS.Prototype_pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    public class Plane : Unit
    {
        //public int PlaneId { get; set; }

        public int PlaneId { get; set; }
        //public override int UnitId { get { return PlaneId; } }
        public override int Size { get; set; }
        public List<PlaneCoordinates> Coordinates { get; set; }
        public override string PlayerName { get; set; }
        public override int BattleId { get; set; }

        public new User Player { get; set; }
        public new Battle Battle { get; set; }

        public int GetId() { return this.PlaneId; }

        /*temorrary*/
        public override string ProductName { get { return "It's Plane"; } }
        public override Unit UnitObj { get { return new Plane(); } }

        public Plane()
        {
            Coordinates = new List<PlaneCoordinates>();
        }
        public Plane(User owner, Battle battle)
        {
            PlayerName = owner.UserName;
            Player = owner;
            BattleId = battle.BattleId;
            Battle = battle;
            Coordinates = new List<PlaneCoordinates>();
        }
        public object ShalowClone()
        {
            return (Plane)this.Clone();
        }
        public Plane DeepClone()
        {
            Plane cloned = (Plane)this.Clone();
            cloned.Battle = this.Battle.Copy();
            cloned.BattleId = this.BattleId;
            cloned.Coordinates = this.Coordinates.ToList<PlaneCoordinates>();
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
    }
}