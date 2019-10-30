using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.Models;
using KronoBattleship.DESIGN_PATTERNS.Prototype_pattern;

namespace KronoBattleship.DESIGN_PATTERNS.Factory
{
	public abstract class Unit : IPrototype
	{
		public object Clone()
		{
			return this.MemberwiseClone();
		}

		public abstract int Size { get; set; }
		public abstract List<PlaneCoordinates> Coordinates { get; set; }
		public abstract string PlayerName { get; set; }
		public abstract int BattleId { get; set; }

		public virtual User Player { get; set; }
		public virtual Battle Battle { get; set; }


        public abstract Unit UnitObj { get; } //temporrary
        public abstract string ProductName { get; } //temporrary
    }
}