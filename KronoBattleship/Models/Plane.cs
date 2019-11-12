using KronoBattleship.DESIGN_PATTERNS.Factory;
using KronoBattleship.DESIGN_PATTERNS.Prototype_pattern;
using KronoBattleship.DESIGN_PATTERNS.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    /// <summary>
    /// Plane class
    /// </summary>
    public class Plane : Unit
    {
        public int PlaneId { get; set; }
        public override int Size { get; set; }
        public override List<PlaneCoordinates> Coordinates { get; set; }
        public override string PlayerName { get; set; }
        public override int BattleId { get; set; }

        public new User Player { get; set; }
        public new Battle Battle { get; set; }

        public override string ProductName { get { return "It's Plane!"; } }
        public override Unit UnitObj { get { return new Plane(); } }

        /// <summary>
        /// Plane constructor
        /// </summary>
        public Plane()
        {
            Coordinates = new List<PlaneCoordinates>();
        }
        /// <summary>
        /// Plane constructor
        /// </summary>
        /// <param name="owner">User</param>
        /// <param name="battle">Battle</param>
        public Plane(User owner, Battle battle)
        {
            PlayerName = owner.UserName;
            Player = owner;
            BattleId = battle.BattleId;
            Battle = battle;
            Coordinates = new List<PlaneCoordinates>();
        }
        public object ShallowClone()
        {
            Plane cloned = (Plane)this.Clone();
            //System.Diagnostics.Debug.WriteLine("ShalowClone() Coordinates");
            //System.Diagnostics.Debug.WriteLine("Origin.Coordinates hash code: {0}", this.Coordinates.GetHashCode());
            //System.Diagnostics.Debug.WriteLine("Cloned.Coordinates hash code: {0}", cloned.Coordinates.GetHashCode());
            return cloned;
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
            //System.Diagnostics.Debug.WriteLine("DeepClone() Coordinates");
            //System.Diagnostics.Debug.WriteLine("Origin.Coordinates hash code: {0}", this.Coordinates.GetHashCode());
            //System.Diagnostics.Debug.WriteLine("Cloned.Coordinates hash code: {0}", cloned.Coordinates.GetHashCode());
            return cloned;
        }
    }
}